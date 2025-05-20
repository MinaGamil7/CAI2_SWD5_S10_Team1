using Tech.DataAccess.Data;
using Tech.Models;
using Microsoft.AspNetCore.Mvc;
using Tech.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Tech.Utility;
using Microsoft.EntityFrameworkCore;
using Tech.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace Tech.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        [BindProperty]
        public UserRoleVM userRoleVM { get; set; }
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RoleManagement(string userId)
        {
            userRoleVM = new UserRoleVM()
            {
                ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, includeProperties: "Company"),
                Roles = _roleManager.Roles.Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                Companies = _unitOfWork.Company.GetAll().Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            userRoleVM.ApplicationUser.Role = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == userId)).GetAwaiter().GetResult().FirstOrDefault();
            return View(userRoleVM);
        }
        [HttpPost]
        public IActionResult RoleManagement()
        {
            string oldRoleName = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == userRoleVM.ApplicationUser.Id)).GetAwaiter().GetResult().FirstOrDefault();
            ApplicationUser userFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == userRoleVM.ApplicationUser.Id);

            if (!(userRoleVM.ApplicationUser.Role == oldRoleName))
            {
                
                if(userRoleVM.ApplicationUser.Role == SD.Role_Company)
                {
                    userFromDb.CompanyId = userRoleVM.ApplicationUser.CompanyId;
                }
                if (oldRoleName == SD.Role_Company)
                {
                    userFromDb.CompanyId = null;
                }
                _unitOfWork.ApplicationUser.Update(userFromDb);
                _unitOfWork.Save();
                _userManager.RemoveFromRoleAsync(userFromDb, oldRoleName).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(userFromDb, userRoleVM.ApplicationUser.Role).GetAwaiter().GetResult();
            }
            else if (userRoleVM.ApplicationUser.Role == SD.Role_Company)
            {
                if (userFromDb.CompanyId != userRoleVM.ApplicationUser.CompanyId)
                {
                    userFromDb.CompanyId = userRoleVM.ApplicationUser.CompanyId;
                    _unitOfWork.ApplicationUser.Update(userFromDb);
                    _unitOfWork.Save();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company");

            foreach (var obj in allObj)
            {
                obj.Role = _userManager.GetRolesAsync(obj).GetAwaiter().GetResult().FirstOrDefault();

                if (obj.Company == null)
                {
                    obj.Company = new() { Name = ""};
                }
            }
            return Json(new { data = allObj });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDb = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking." });
            }

            bool isLocked = objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now;
            if (isLocked)
            {
                // User is currently locked and we need to unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _unitOfWork.ApplicationUser.Update(objFromDb);
            _unitOfWork.Save();
            if (isLocked) { return Json(new { success = true, message = $"{objFromDb.Name} Unlocked Successfuly" }); }
            else { return Json(new { success = true, message = $"{objFromDb.Name} Locked Successfuly" }); }

        }
        #endregion
    }
}
