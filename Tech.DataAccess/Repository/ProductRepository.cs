using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.DataAccess.Data;
using Tech.DataAccess.Repository.IRepository;
using Tech.Models;

namespace Tech.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.Id == product.Id);
            if (objFromDb != null)
            {
                //if (product.ImageUrl != null)
                //{
                //    objFromDb.ImageUrl = product.ImageUrl;
                //}
                objFromDb.Name = product.Name;
                objFromDb.Description = product.Description;
                objFromDb.Price = product.Price;
                objFromDb.DiscountedPrice = product.DiscountedPrice;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.ProductImages = product.ProductImages;
            }
        }
    }
}
