using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EnityTypeConfiguration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Id).IsUnique();
            
            // Поля
            builder.Property(p => p.Name)
                   .HasMaxLength(64)
                   .IsRequired();

            builder.Property(p => p.Cost)
                   .IsRequired();

            builder.Property(p => p.Description)
                   .HasMaxLength(512)
                   .HasDefaultValue(string.Empty);

            builder.Property(p => p.TypeId);

            // Внешние ключи
            builder.HasMany(product => product.PropertyValues)
                   .WithOne(propertyValues => propertyValues.Product)
                   .HasForeignKey(propertyValues => propertyValues.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(product => product.Type)
                   .WithMany(type => type.Products)
                   .HasForeignKey(product => product.TypeId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(product => product.Images)
                   .WithOne(productImages => productImages.Product)
                   .HasForeignKey(productImages => productImages.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
