using ECommerceOrder.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace ECommerceOrder.Data
{
    public class AppDbContext:  DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
            public DbSet<Product> Products { get; set; }
            public DbSet <Category>Categories { get; set; }

            public DbSet <Tag> Tags { get; set; }

            public DbSet<ProductTag> ProductTags { get; set; }

            public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }


    }
}
