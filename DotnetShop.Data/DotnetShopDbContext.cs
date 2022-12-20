using DotnetShop.Data.Domain;
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
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var random = new Random();

            modelBuilder.Entity<Customer>()
                .HasOne(x => x.ApplicationUser)
                .WithOne(x => x.Customer)
                .HasForeignKey<Customer>(x => x.ApplicationUserId);

            modelBuilder.Entity<Product>().HasMany(p => p.Warehouses).WithMany(w => w.Products)
                .UsingEntity<ProductWarehouse>(pw => pw.HasOne<Warehouse>().WithMany(), pw => pw.HasOne<Product>().WithMany())
                .Property(pw => pw.ProductQuantity).HasDefaultValue(0);

            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Shirt", Description = "" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Cup", Description = "" });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Badge", Description = "" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
