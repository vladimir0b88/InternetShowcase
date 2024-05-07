using Application.Common.Interfaces;
using Application.Models.Products.Create;
using Application.Models.Products.Update;
using BlazorWebAssembly;
using BlazorWebAssembly.Services;
using BlazorWebAssembly.Validators.Products;
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

#endregion

#region Validators
// Product
builder.Services.AddScoped<IValidator<ProductCreateDto>,ProductCreateDtoClientValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDto>,ProductUpdateDtoClientValidator>();

#endregion

await builder.Build().RunAsync();
