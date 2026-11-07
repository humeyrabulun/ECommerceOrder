using ECommerceOrder.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceOrderApi.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
       public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.HasData(
                 new Category { Id = 1, Name = "Elektronik", Description = "Teknoloji ürünleri" },
                 new Category { Id = 2, Name = "Giyim", Description = "Kıyafet ve aksesuarlar" },
                 new Category { Id = 3, Name = "Kitap", Description = "Kitaplar" },
                new Category { Id = 4, Name = "Kozmetik", Description = "Kozmetik ürünleri" },
                new Category { Id = 5, Name = "Spor", Description = "Spor ekipmanları" }

                 );
        }
    }
}
