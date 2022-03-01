using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantAppData.DbModels;

namespace RestaurantAppData.DataContext
{
    public class RestaurantAppDbContext:IdentityDbContext
    {
        public RestaurantAppDbContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } //DbSet = databasede olan tabloları simgeler
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}
