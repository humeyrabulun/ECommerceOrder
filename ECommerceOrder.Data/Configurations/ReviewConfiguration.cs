using ECommerceOrder.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceOrderApi.Configurations

{
    public class ReviewConfiguration
    {
        public void Configure (EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Comment)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Review { Id = 1, ProductId = 1, Rating = 5, Comment = "Harika bir bilgisayar, uçuyor!" },
                new Review { Id = 2, ProductId = 1, Rating = 3, Comment = "Fena değil ama fanı sesli." },
                new Review { Id = 3, ProductId = 2, Rating = 4, Comment = "Klavye hissiyatı çok iyi." }
            );
        }
    }
}
