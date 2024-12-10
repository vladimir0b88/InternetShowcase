using Application.Common;
using Application.Models;
using Domain.Entities;
using ErrorOr;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Common
{
    public class PropertyValueHttpService(IHttpClientFactory httpClientFactory,
                                          IValidator<PropertyValueUpdateDto> updateValidator,
                                          IValidator<PropertyValueUpdateDtoList> updateListValidator) : IPropertyValueService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");

        private const string _controllerUri = "api/PropertyValues";

        public async Task<ErrorOr<List<PropertyValue>>> GetAllAsync()
        {
            var response = await httpClient.GetAsync(_controllerUri);

            var result = await HttpResponseHandler.GetResultAsync<List<PropertyValue>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<List<PropertyValue>>> GetByProductIdAsync(long productId)
        {
            var response = await httpClient.GetAsync($"{_controllerUri}/{productId}");

            var result = await HttpResponseHandler.GetResultAsync<List<PropertyValue>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(PropertyValueUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();

            var response = await httpClient.PutAsJsonAsync(_controllerUri, updateDto);

            var result = await HttpResponseHandler.GetResultAsync<Updated>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Updated>> UpdateListAsync(PropertyValueUpdateDtoList updateDtoList)
        {
            var validationResult = await updateListValidator.ValidateAsync(updateDtoList);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();

            var response = await httpClient.PutAsJsonAsync($"{_controllerUri}/List", updateDtoList);

            var result = await HttpResponseHandler.GetResultAsync<Updated>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<List<UniquePropertyValues>>> GetUniquesByProductTypeIdAsync(long productTypeId)
        {
            var response = await httpClient.GetAsync($"{_controllerUri}/ProductType/{productTypeId}/UniqueValues");

            var result = await HttpResponseHandler.GetResultAsync<List<UniquePropertyValues>>(response);

            return result;
        }
    }
}
