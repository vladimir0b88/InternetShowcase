using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EnityTypeConfiguration
{
    internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(pi => pi.Id);
            builder.HasIndex(pi => pi.Id);

            // Поля
            builder.Property(pi => pi.ProductId)
                   .IsRequired();

            builder.Property(pi => pi.Image)
                   .IsRequired();

            // Внешние ключи
            builder.HasOne(productImage => productImage.Product)
                   .WithMany(product => product.Images)
                   .HasForeignKey(productImage => productImage.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
