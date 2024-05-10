using Application.Common.Errors;
using Application;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.TypeProperties.Create;
using Application.Models.TypeProperties.Update;
using BlazorWebAssembly.Common;
using Domain.Entities;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class TypePropertyHttpService(HttpClient httpClient,
                                         IValidator<TypePropertyCreateDto> createValidator,
                                         IValidator<TypePropertyUpdateDto> updateValidator) : ITypePropertyService
    {
        private const string _controllerUri = "api/TypeProperties";

        public async Task<Result<List<TypeProperty>>> GetPropertiesByProductTypeId(long typeId)
        {
            var response = await httpClient.GetAsync($"{_controllerUri}/ProductType/{typeId}");

            var result = await HttpResponseHandler.GetResult<List<TypeProperty>>(response);

            return result;
        }
        public async Task<Result> AddProperty(TypePropertyCreateDto createDto)
        {
            var validationResult = await createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Свойство типа товара для создания не прошло валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            var response = await httpClient.PostAsJsonAsync(_controllerUri, createDto);

            var result = await HttpResponseHandler.GetResult(response);

            return result;
        }
        public async Task<Result> UpdateProperty(TypePropertyUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Свойство типа товара для создания не прошло валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);
            
            var response = await httpClient.PutAsJsonAsync(_controllerUri, updateDto);

            var result = await HttpResponseHandler.GetResult(response);

            return result;
        }

        public async Task<Result> DeleteProperty(long propertyId)
        {
            var response = await httpClient.DeleteAsync($"{_controllerUri}/{propertyId}");

            var result = await HttpResponseHandler.GetResult(response);

            return result;
        }
    }
}
