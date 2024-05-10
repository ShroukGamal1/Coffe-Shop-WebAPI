using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Coffe_Shop_WebAPI.Models
{
    public class CoffeeContext : IdentityDbContext<AppUser>
    {
        public DbSet<AppUser> Users {  get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product_User> Products_User { get; set;}
        public DbSet<Product_Order> Product_Orders { get; set; }

        public CoffeeContext()
        {
            
        }
        public CoffeeContext(DbContextOptions<CoffeeContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product_Order>()
    .HasKey("ProductId", "OrderId");
            builder.Entity<Product_User>()
    .HasKey("ProductId", "UserId");
            //builder.Entity<AppUser>().HasNoKey().ToView("");

            base.OnModelCreating(builder);
        }

    }
}
