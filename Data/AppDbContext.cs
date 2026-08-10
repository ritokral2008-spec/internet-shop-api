using InternetShop.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("users");

            modelBuilder.Entity<Product>()
                .ToTable("products");

            modelBuilder.Entity<Category>()
                .ToTable("categories");

            modelBuilder.Entity<Order>()
                .ToTable("orders");

            modelBuilder.Entity<OrderItem>()
                .ToTable("order_items");
        }
    }
}
