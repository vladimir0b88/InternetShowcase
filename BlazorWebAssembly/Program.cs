global using Microsoft.AspNetCore.Components.Authorization;
using Application.Common;
using Application.Models;
using Blazored.LocalStorage;
using BlazorWebAssembly;
using BlazorWebAssembly.Common;
using BlazorWebAssembly.Services;
using BlazorWebAssembly.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5297")
});
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();


#region Services
builder.Services.AddScoped<IProductService, ProductHttpService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeHttpService>();
builder.Services.AddScoped<ITypePropertyService, TypePropertyHttpService>();
builder.Services.AddScoped<IPropertyValueService, PropertyValueHttpService>();
builder.Services.AddScoped<IUserService, UserHttpService>();

#endregion

#region Validators
// Product
builder.Services.AddScoped<IValidator<ProductCreateDto>, ProductCreateDtoClientValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateDtoClientValidator>();

// ProductType
builder.Services.AddScoped<IValidator<ProductTypeCreateDto>, ProductTypeCreateDtoClientValidator>();
builder.Services.AddScoped<IValidator<ProductTypeUpdateDto>, ProductTypeUpdateDtoClientValidator>();

// TypeProperty
builder.Services.AddScoped<IValidator<TypePropertyCreateDto>, TypePropertyCreateDtoClientValidator>();
builder.Services.AddScoped<IValidator<TypePropertyUpdateDto>, TypePropertyUpdateDtoClientValidator>();

// PropertyValue
builder.Services.AddScoped<IValidator<PropertyValueUpdateDto>, PropertyValueUpdateDtoClientValidator>();
builder.Services.AddScoped<IValidator<PropertyValueUpdateDtoList>, PropertyValueUpdateDtoListClientValidator>();

// User
builder.Services.AddScoped<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();


#endregion

await builder.Build().RunAsync();
