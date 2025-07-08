using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EnityTypeConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Id).IsUnique();

            builder.Property(u => u.UserName)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            builder.Property(u => u.Email) 
                   .HasMaxLength(64)
                   .IsRequired();

            builder.Property(u => u.Role)
                    .HasMaxLength(30)
                    .IsRequired()
                    .HasDefaultValue(Roles.Guest);
        }
    }
}
