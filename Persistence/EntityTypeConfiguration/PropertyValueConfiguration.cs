using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EnityTypeConfiguration
{
    public class PropertyValueConfiguration : IEntityTypeConfiguration<PropertyValue>
    {
        public void Configure(EntityTypeBuilder<PropertyValue> builder)
        {
            builder.ToTable("PropertyValues");

            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Id).IsUnique();

            // Поля
            builder.Property(p => p.PropertyId).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();

            builder.Property(p => p.Value)
                   .HasMaxLength(64)
                   .HasDefaultValue(string.Empty);

            // Внешние ключи
            builder.HasOne(propertyValue => propertyValue.TypeProperty)
                   .WithMany(property => property.PropertyValues)
                   .HasForeignKey(propertyValue => propertyValue.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(propertyValue => propertyValue.Product)
                   .WithMany(product => product.PropertyValues)
                   .HasForeignKey(propertyValue => propertyValue.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
