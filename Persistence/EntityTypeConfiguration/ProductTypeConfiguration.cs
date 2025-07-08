using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EnityTypeConfiguration
{
    internal class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("ProductTypes");

            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Id).IsUnique();

            // Поля
            builder.Property(p => p.Name)
                   .HasMaxLength(64)
                   .IsRequired();

            // Внешние ключи
            builder.HasMany(type => type.Products)
                   .WithOne(product => product.Type)
                   .HasForeignKey(product => product.TypeId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(type => type.Properties)
                   .WithOne(properties => properties.Type)
                   .HasForeignKey(properties => properties.TypeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
