using Application.Common;
using Application.Models;
using BlazorWebAssembly.Common;
using Domain.Entities;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class ProductHttpService(HttpClient httpClient,
                                    IValidator<ProductCreateDto> createValidator,
                                    IValidator<ProductUpdateDto> updateValidator) : IProductService
    {
        private const string _controllerUri = "api/Products";

        public async Task<Result<List<Product>>> GetAllProducts()
        {
            var response = await httpClient.GetAsync(_controllerUri);

            var result = await HttpResponseHandler.GetResult<List<Product>>(response);

            return result;
        }

        public async Task<Result<List<Product>>> GetByProductTypeId(long productTypeId)
        {
            var response = await httpClient.GetAsync($"{_controllerUri}/ProductType/{productTypeId}");

            var result = await HttpResponseHandler.GetResult<List<Product>>(response);

            return result;
        }

        public async Task<Result<Product>> GetProductById(long id)
        {
            var response = await httpClient.GetAsync($"{_controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResult<Product>(response);

            return result;
        }

        public async Task<Result> AddProduct(ProductCreateDto productDto)
        {
            var validationResult = await createValidator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Продукт для создания не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            var response = await httpClient.PostAsJsonAsync(_controllerUri, productDto);

            var result = await HttpResponseHandler.GetResult(response);

            return result;
        }

        public async Task<Result> UpdateProduct(ProductUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Продукт для модификации не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);


            var response = await httpClient.PutAsJsonAsync(_controllerUri, updateDto);

            var result = await HttpResponseHandler.GetResult(response);

            return result;
        }

        public async Task<Result> DeleteProductById(long id)
        {
            var response = await httpClient.DeleteAsync($"{_controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResult(response);

            return result;
        }
    }
}
