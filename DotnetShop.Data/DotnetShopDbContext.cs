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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(x => x.ApplicationUser)
                .WithOne(x => x.Customer)
                .HasForeignKey<Customer>(x => x.ApplicationUserId);

            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Shirt" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Cup" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Badge" });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
