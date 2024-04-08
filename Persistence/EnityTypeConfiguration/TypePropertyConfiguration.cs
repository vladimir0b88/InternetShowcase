
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EnityTypeConfiguration
{
    internal class TypePropertyConfiguration : IEntityTypeConfiguration<TypeProperty>
    {
        public void Configure(EntityTypeBuilder<TypeProperty> builder)
        {
            builder.ToTable("TypeProperties");

            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Id).IsUnique();

            // Поля
            builder.Property(p => p.Name)
                   .HasMaxLength(64)
                   .IsRequired();

            builder.Property(t => t.TypeId);

            // Внешние ключи
            builder.HasOne(property => property.Type)
                   .WithMany(type => type.Properties)
                   .HasForeignKey(property => property.TypeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(property => property.PropertyValues)
                   .WithOne(propertyValues => propertyValues.TypeProperty)
                   .HasForeignKey(propertyValues => propertyValues.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
