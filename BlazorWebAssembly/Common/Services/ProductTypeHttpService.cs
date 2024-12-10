using Application.Common;
using Application.Models;
using Domain.Entities;
using ErrorOr;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Common
{
    public class ProductTypeHttpService(IHttpClientFactory httpClientFactory,
                                        IValidator<ProductTypeAddDto> createValidator,
                                        IValidator<ProductTypeUpdateDto> updateValidator) : IProductTypeService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");

        private const string _controllerUri = "api/ProductTypes";

        public async Task<ErrorOr<List<ProductType>>> GetAllAsync()
        {
            var response = await httpClient.GetAsync(_controllerUri);

            var result = await HttpResponseHandler.GetResultAsync<List<ProductType>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<ProductType>> GetByIdAsync(long id)
        {
            var response = await httpClient.GetAsync($"{_controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResultAsync<ProductType>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Created>> AddAsync(ProductTypeAddDto dto)
        {
            var validationResult = await createValidator.ValidateAsync(dto);

            if(!validationResult.IsValid)
                return validationResult.GetGeneralError();

            var response = await httpClient.PostAsJsonAsync(_controllerUri, dto);

            var result = await HttpResponseHandler.GetResultAsync<Created>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(ProductTypeUpdateDto dto)
        {
            var validationResult = await updateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();

            var response = await httpClient.PutAsJsonAsync(_controllerUri, dto);

            var result = await HttpResponseHandler.GetResultAsync<Updated>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(long id)
        {
            var response = await httpClient.DeleteAsync($"{_controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResultAsync<Deleted>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }
    }
}
