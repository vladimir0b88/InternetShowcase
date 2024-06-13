using Application.Common;
using Application.Models;
using Application.Services;
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
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateDtoValidator>();
            services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateDtoValidator>();

            // ProductType
            services.AddScoped<IValidator<ProductTypeCreateDto>, ProductTypeCreateDtoValidator>();
            services.AddScoped<IValidator<ProductTypeUpdateDto>, ProductTypeUpdateDtoValidator>();

            // TypeProperty
            services.AddScoped<IValidator<TypePropertyCreateDto>, TypePropertyCreateDtoValidator>();
            services.AddScoped<IValidator<TypePropertyUpdateDto>, TypePropertyUpdateDtoValidator>();

            // PropertyValue
            services.AddScoped<IValidator<PropertyValueUpdateDto>, PropertyValueUpdateDtoValidator>();
            services.AddScoped<IValidator<PropertyValueUpdateDtoList>, PropertyValueUpdateDtoListValidator>();
            
            // User
            services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
            
            // ProductImage
            services.AddScoped<IValidator<ProductImageAddDto>, ProductImageAddDtoValidator>();

            #endregion

            return services;
        }
    }
}
