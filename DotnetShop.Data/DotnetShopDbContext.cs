using DotnetShop.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotnetShop.Data
{
    public class DotnetShopDbContext : IdentityDbContext
    {
        public DotnetShopDbContext(DbContextOptions<DotnetShopDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Shirt" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Cup" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Badge" });
        }
    }
}
