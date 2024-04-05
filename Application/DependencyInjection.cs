using Application.Common.Interfaces;
using Application.Models.Products.Create;
using Application.Models.Products.Update;
using Application.Models.ProductTypes.Create;
using Application.Models.ProductTypes.Update;
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

            #endregion


            #region Validators
            //Product
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateDtoValidator>();
            services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateDtoValidator>();

            //ProductType
            services.AddScoped<IValidator<ProductTypeCreateDto>, ProductTypeCreateDtoValidator>();
            services.AddScoped<IValidator<ProductTypeUpdateDto>, ProductTypeUpdateDtoValidator>();

            #endregion

            return services;
        }
    }
}
