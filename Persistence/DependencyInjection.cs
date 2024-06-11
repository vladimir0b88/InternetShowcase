using Application.Common;
using Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
                                                             IConfiguration configuration)
        {
            string connectionString = configuration["DbConnection"]!;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });


            // Репозитории
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<ITypePropertyRepository, TypePropertyRepository>();
            services.AddScoped<IPropertyValueRepository, PropertyValueRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
