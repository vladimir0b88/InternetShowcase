using Application.Common.Interfaces;
using Application.Models.Products.Create;
using Application.Models.Products.Update;
using Application.Models.ProductTypes.Create;
using Application.Models.ProductTypes.Update;
using Application.Models.PropertyValues.Update;
using Application.Models.TypeProperties.Create;
using Application.Models.TypeProperties.Update;
using Application.Models.Users.Register;
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

            // User
            services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();

            #endregion

            return services;
        }
    }
}
