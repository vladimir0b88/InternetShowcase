using Application.Common.Interfaces;
using Application.Models.Products.Create;
using Application.Models.Products.Update;
using Application.Models.ProductTypes.Create;
using Application.Models.ProductTypes.Update;
using Application.Models.PropertyValues.Update;
using Application.Models.TypeProperties.Create;
using Application.Models.TypeProperties.Update;
using BlazorWebAssembly;
using BlazorWebAssembly.Services;
using BlazorWebAssembly.Validators.Products;
using BlazorWebAssembly.Validators.ProductTypes;
using BlazorWebAssembly.Validators.PropertyValues;
using BlazorWebAssembly.Validators.TypeProperties;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped(sp => new HttpClient 
{
    BaseAddress = new Uri("http://localhost:5297")
});


#region Services
builder.Services.AddScoped<IProductService, ProductHttpService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeHttpService>();
builder.Services.AddScoped<ITypePropertyService, TypePropertyHttpService>();
builder.Services.AddScoped<IPropertyValueService, PropertyValueHttpService>();

#endregion

#region Validators
// Product
builder.Services.AddScoped<IValidator<ProductCreateDto>,ProductCreateDtoClientValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDto>,ProductUpdateDtoClientValidator>();

// ProductType
builder.Services.AddScoped<IValidator<ProductTypeCreateDto>, ProductTypeCreateDtoClientValidator>();
builder.Services.AddScoped<IValidator<ProductTypeUpdateDto>, ProductTypeUpdateDtoClientValidator>();

// TypeProperty
builder.Services.AddScoped<IValidator<TypePropertyCreateDto>, TypePropertyCreateDtoClientValidator>();
builder.Services.AddScoped<IValidator<TypePropertyUpdateDto>, TypePropertyUpdateDtoClientValidator>();

// PropertyValue
builder.Services.AddScoped<IValidator<PropertyValueUpdateDto>, PropertyValueUpdateDtoClientValidator>();

#endregion

await builder.Build().RunAsync();
