using Application.Common;
using Application.Models;
using BlazorWebAssembly.Common;
using Domain.Entities;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class PropertyValueHttpService(IHttpClientFactory httpClientFactory,
                                          IValidator<PropertyValueUpdateDto> updateValidator,
                                          IValidator<PropertyValueUpdateDtoList> updateListValidator) : IPropertyValueService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");

        private const string _controllerUri = "api/PropertyValues";

        public async Task<Result<List<PropertyValue>>> GetAllPropertyValues()
        {
            var response = await httpClient.GetAsync(_controllerUri);

            var result = await HttpResponseHandler.GetResult<List<PropertyValue>>(response);

            return result;
        }

        public async Task<Result<List<PropertyValue>>> GetPropertyValuesByProductId(long productId)
        {
            var response = await httpClient.GetAsync($"{_controllerUri}/{productId}");

            var result = await HttpResponseHandler.GetResult<List<PropertyValue>>(response);

            return result;
        }

        public async Task<Result> UpdatePropertyValue(PropertyValueUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Значение свойства товара для изменения не прошло валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            var response = await httpClient.PutAsJsonAsync(_controllerUri, updateDto);

            var result = await HttpResponseHandler.GetResult(response);

            return result;
        }

        public async Task<Result> UpdatePropertyValueList(PropertyValueUpdateDtoList updateDtoList)
        {
            var validationResult = await updateListValidator.ValidateAsync(updateDtoList);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Значения свойств товара для изменения не прошли валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            var response = await httpClient.PutAsJsonAsync($"{_controllerUri}/List", updateDtoList);

            var result = await HttpResponseHandler.GetResult(response);

            return result;
        }
    }
}
