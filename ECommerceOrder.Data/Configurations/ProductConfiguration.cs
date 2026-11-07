using ECommerceOrder.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceOrderApi.Configurations
{
    public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.CategoryId);
                

            builder.Property(p=>p.Name)
                 .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p=>p.Barcode)
                .HasMaxLength(50);

            builder.HasOne(p=>p.Category)
                .WithMany(c=> c.Products)
                .HasForeignKey(p=> p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            var products = new List<Product>();
            for (int i = 1; i <= 30; i++)
            {
                products.Add(new Product
                {
                    Id = i,
                    Name = $"Product {i}",
                    Price=i*100,
                    CategoryId = (i % 5) + 1 
                });
            }
        }
    }
}
