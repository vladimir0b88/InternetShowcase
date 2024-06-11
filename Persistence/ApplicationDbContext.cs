using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.EnityTypeConfiguration;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<TypeProperty> TypeProperties { get; set; }

        public DbSet<PropertyValue> PropertyValues { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductTypeConfiguration());
            builder.ApplyConfiguration(new TypePropertyConfiguration());
            builder.ApplyConfiguration(new PropertyValueConfiguration());
            builder.ApplyConfiguration(new ProductImageConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
