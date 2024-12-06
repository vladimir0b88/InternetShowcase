using Application.Common;
using Application.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            
            #region Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<ITypePropertyService, TypePropertyService>();
            services.AddScoped<IPropertyValueService, PropertyValueService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductImageService, ProductImageService>();

            #endregion


            #region Validators
            // Product
            services.AddScoped<IValidator<ProductAddDto>, ProductAddDtoValidator>();
            services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateDtoValidator>();

            // ProductType
            services.AddScoped<IValidator<ProductTypeAddDto>, ProductTypeAddDtoValidator>();
            services.AddScoped<IValidator<ProductTypeUpdateDto>, ProductTypeUpdateDtoValidator>();

            // TypeProperty
            services.AddScoped<IValidator<TypePropertyAddDto>, TypePropertyAddDtoValidator>();
            services.AddScoped<IValidator<TypePropertyUpdateDto>, TypePropertyUpdateDtoValidator>();

            // PropertyValue
            services.AddScoped<IValidator<PropertyValueUpdateDto>, PropertyValueUpdateDtoValidator>();
            services.AddScoped<IValidator<PropertyValueUpdateDtoList>, PropertyValueUpdateDtoListValidator>();
            
            // User
            services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
            
            // ProductImage
            services.AddScoped<IValidator<ProductImageAddDto>, ProductImageAddDtoValidator>();

            // Filters
            services.AddScoped<IValidator<ProductsFilter>, ProductsFilterValidator>();
            services.AddScoped<IValidator<PropertyFilter>, PropertyFilterValidator>();

            #endregion

            return services;
        }
    }
}
