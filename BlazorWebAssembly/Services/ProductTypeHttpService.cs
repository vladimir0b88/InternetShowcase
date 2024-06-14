using Application.Common;
using Application.Models;
using BlazorWebAssembly.Common;
using Domain.Entities;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class ProductTypeHttpService(IHttpClientFactory httpClientFactory,
                                        IValidator<ProductTypeCreateDto> createValidator,
                                        IValidator<ProductTypeUpdateDto> updateValidator) : IProductTypeService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");

        private const string _controllerUri = "api/ProductTypes";

        public async Task<Result<List<ProductType>>> GetAllProductTypes()
        {
            var response = await httpClient.GetAsync(_controllerUri);

            var result = await HttpResponseHandler.GetResult<List<ProductType>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result<ProductType>> GetProductTypeById(long id)
        {
            var response = await httpClient.GetAsync($"{_controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResult<ProductType>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result> AddProductType(ProductTypeCreateDto dto)
        {
            var validationResult = await createValidator.ValidateAsync(dto);

            if(!validationResult.IsValid)
                return new ValidationErrorResult(message: "Тип товара для создания не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            var response = await httpClient.PostAsJsonAsync(_controllerUri, dto);

            var result = await HttpResponseHandler.GetResult(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result> UpdateProductType(ProductTypeUpdateDto dto)
        {
            var validationResult = await updateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Тип товара для изменения не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            var response = await httpClient.PutAsJsonAsync(_controllerUri, dto);

            var result = await HttpResponseHandler.GetResult(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<Result> DeleteProductTypeById(long id)
        {
            var response = await httpClient.DeleteAsync($"{_controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResult(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }
    }
}
