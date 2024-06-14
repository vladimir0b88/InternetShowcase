using Application.Common;
using Application.Models;
using BlazorWebAssembly.Common;
using Domain.Entities;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class ProductHttpService(IHttpClientFactory httpClientFactory,
                                    IValidator<ProductCreateDto> createValidator,
                                    IValidator<ProductUpdateDto> updateValidator) : IProductService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");
        
        private const string controllerUri = "api/Products";

        public async Task<Result<List<Product>>> GetAllProducts()
        {
            var response = await httpClient.GetAsync(controllerUri);

            var result = await HttpResponseHandler.GetResult<List<Product>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result<List<Product>>> GetByProductTypeId(long productTypeId)
        {
            var response = await httpClient.GetAsync($"{controllerUri}/ProductType/{productTypeId}");

            var result = await HttpResponseHandler.GetResult<List<Product>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result<Product>> GetProductById(long id)
        {
            var response = await httpClient.GetAsync($"{controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResult<Product>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result> AddProduct(ProductCreateDto productDto)
        {
            var validationResult = await createValidator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Продукт для создания не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            var response = await httpClient.PostAsJsonAsync(controllerUri, productDto);

            var result = await HttpResponseHandler.GetResult(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result> UpdateProduct(ProductUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Продукт для модификации не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);


            var response = await httpClient.PutAsJsonAsync(controllerUri, updateDto);

            var result = await HttpResponseHandler.GetResult(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result> DeleteProductById(long id)
        {
            var response = await httpClient.DeleteAsync($"{controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResult(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }
    }
}
