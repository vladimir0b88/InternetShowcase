
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

            builder.Property(p => p.Name)
                   .HasMaxLength(64);

            builder.Property(t => t.TypeId);
        }
    }
}
