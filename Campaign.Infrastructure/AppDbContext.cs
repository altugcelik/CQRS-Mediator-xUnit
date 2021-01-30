using Campaign.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Campaign.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Campaigning> Campaigns { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}