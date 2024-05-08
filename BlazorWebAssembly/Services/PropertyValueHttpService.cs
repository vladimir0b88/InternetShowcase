using Application.Common.Errors;
using Application;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.PropertyValues.Update;
using BlazorWebAssembly.Common;
using Domain.Entities;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class PropertyValueHttpService(HttpClient httpClient,
                                          IValidator<PropertyValueUpdateDto> updateValidator) : IPropertyValueService
    {
        private const string _controllerUri = "api/PropertyValues";

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
    }
}
