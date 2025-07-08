using Application.Common;
using Application.Models;
using Domain.Entities;
using ErrorOr;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Common
{
    public class TypePropertyHttpService(IHttpClientFactory httpClientFactory,
                                         IValidator<TypePropertyAddDto> createValidator,
                                         IValidator<TypePropertyUpdateDto> updateValidator) : IPropertyService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");

        private const string _controllerUri = "api/TypeProperties";

        public async Task<ErrorOr<List<ProductTypeProperty>>> GetPropertyByProductTypeIdAsync(long typeId)
        {
            var response = await httpClient.GetAsync($"{_controllerUri}/ProductType/{typeId}");

            var result = await HttpResponseHandler.GetResultAsync<List<ProductTypeProperty>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }
        public async Task<ErrorOr<Created>> AddPropertyAsync(TypePropertyAddDto createDto)
        {
            var validationResult = await createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();

            var response = await httpClient.PostAsJsonAsync(_controllerUri, createDto);

            var result = await HttpResponseHandler.GetResultAsync<Created>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }
        public async Task<ErrorOr<Updated>> UpdatePropertyAsync(TypePropertyUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();

            var response = await httpClient.PutAsJsonAsync(_controllerUri, updateDto);

            var result = await HttpResponseHandler.GetResultAsync<Updated>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeletePropertyAsync(long propertyId)
        {
            var response = await httpClient.DeleteAsync($"{_controllerUri}/{propertyId}");

            var result = await HttpResponseHandler.GetResultAsync<Deleted>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<List<ProductTypeProperty>>> GetAllPropertiesAsync()
        {
            var response = await httpClient.GetAsync(_controllerUri);

            var result = await HttpResponseHandler.GetResultAsync<List<ProductTypeProperty>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<ProductTypeProperty>> GetPropertyByIdAsync(long propertyId)
        {
            var response = await httpClient.GetAsync($"{_controllerUri}/{propertyId}");

            var result = await HttpResponseHandler.GetResultAsync<ProductTypeProperty>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }
    }
}
