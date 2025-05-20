using Tech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tech.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Computers & Accessories", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Mobile Phones & Accessories", DisplayOrder = 2 },
                new Category { Id = 3, Name = "TV & Home Entertainment", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Microsoft",
                    StreetAddress = "123 Redmond Way",
                    City = "Redmond",
                    PostalCode = "98052",
                    PhoneNumber = "425-882-8080"
                },
                new Company
                {
                    Id = 2,
                    Name = "Google",
                    StreetAddress = "1600 Amphitheatre Parkway",
                    City = "Mountain View",
                    PostalCode = "94043",
                    PhoneNumber = "650-253-0000"
                },
                new Company
                {
                    Id = 3,
                    Name = "Amazon",
                    StreetAddress = "410 Terry Ave. North",
                    City = "Seattle",
                    PostalCode = "98109",
                    PhoneNumber = "206-266-1000"
                }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Dell XPS 13 Laptop",
                    Description = "Powerful and compact 13-inch laptop with Intel Core i7, 16GB RAM, and 512GB SSD. Ideal for productivity and portability.",
                    Price = 1299,
                    DiscountedPrice = 1199,
                    CategoryId = 1,
                },
                new Product
                {
                    Id = 2,
                    Name = "HP Omen Gaming Desktop",
                    Description = "High-performance gaming desktop featuring AMD Ryzen 7, 32GB RAM, NVIDIA RTX 4070, and 1TB SSD for ultimate performance.",
                    Price = 1599,
                    DiscountedPrice = 1499,
                    CategoryId = 1,
                },
                new Product
                {
                    Id = 3,
                    Name = "Apple MacBook Air M2",
                    Description = "Ultra-thin 13-inch MacBook Air with Apple M2 chip, 8GB RAM, and 256GB SSD. Sleek, fast, and efficient for daily tasks.",
                    Price = 1099,
                    DiscountedPrice = 1049,
                    CategoryId = 1,
                },
                new Product
                {
                    Id = 4,
                    Name = "Logitech MX Master 3S Mouse",
                    Description = "Ergonomic wireless mouse with advanced tracking, customizable buttons, and USB-C fast charging. Designed for professionals.",
                    Price = 99,
                    DiscountedPrice = 89,
                    CategoryId = 2,
                },
                new Product
                {
                    Id = 5,
                    Name = "Acer Predator Helios 16",
                    Description = "Gaming laptop with Intel Core i9, 32GB RAM, RTX 4080, and 1TB SSD. Smooth performance for AAA titles and multitasking.",
                    Price = 1999,
                    DiscountedPrice = 1899,
                    CategoryId = 1,
                },
                new Product
                {
                    Id = 6,
                    Name = "Samsung 27'' 4K UHD Monitor",
                    Description = "Crisp 4K display with HDR10, 60Hz refresh rate, and ultra-slim design. Ideal for creators and professionals.",
                    Price = 349,
                    DiscountedPrice = 319,
                    CategoryId = 3,
                }
                );
        }
    }
    
}

