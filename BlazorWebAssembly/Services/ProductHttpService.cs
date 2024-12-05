using Application.Common;
using Application.Models;
using BlazorWebAssembly.Common;
using Domain.Entities;
using ErrorOr;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class ProductHttpService(IHttpClientFactory httpClientFactory,
                                    IValidator<ProductCreateDto> createValidator,
                                    IValidator<ProductUpdateDto> updateValidator,
                                    IValidator<ProductsFilter> filterValidator) : IProductService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");
        
        private const string controllerUri = "api/Products";

        public async Task<ErrorOr<List<Product>>> GetAllProducts()
        {
            var response = await httpClient.GetAsync(controllerUri);

            var result = await HttpResponseHandler.GetResult<List<Product>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<List<Product>>> GetByProductTypeId(long productTypeId)
        {
            var response = await httpClient.GetAsync($"{controllerUri}/ProductType/{productTypeId}");

            var result = await HttpResponseHandler.GetResult<List<Product>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Product>> GetProductById(long id)
        {
            var response = await httpClient.GetAsync($"{controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResult<Product>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Created>> AddProduct(ProductCreateDto productDto)
        {
            var validationResult = await createValidator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));

            var response = await httpClient.PostAsJsonAsync(controllerUri, productDto);

            var result = await HttpResponseHandler.GetResult(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr> UpdateProduct(ProductUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));


            var response = await httpClient.PutAsJsonAsync(controllerUri, updateDto);

            var result = await HttpResponseHandler.GetResult(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteProductById(long id)
        {
            var response = await httpClient.DeleteAsync($"{controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResult(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<FilteringResult<Product>>> GetProductsByFilter(ProductsFilter filter)
        {
            var validationResult = await filterValidator.ValidateAsync(filter);

            if (!validationResult.IsValid)
                return validationResult.Errors.ConvertAll(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage));

            var response = await httpClient.PostAsJsonAsync($"{controllerUri}/Filter", filter);

            var result = await HttpResponseHandler.GetResult<FilteringResult<Product>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }
    }
}
