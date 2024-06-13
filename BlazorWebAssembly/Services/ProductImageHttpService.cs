using Application.Common;
using Application.Models;
using BlazorWebAssembly.Common;
using Domain.Entities;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class ProductImageHttpService(IHttpClientFactory httpClientFactory,
                                         IValidator<ProductImageAddDto> addValidator) : IProductImageService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");

        private const string controllerUri = "api/ProductImages";

        public async Task<Result> AddImage(ProductImageAddDto addDto)
        {
            var validationResult = await addValidator.ValidateAsync(addDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Изображение для добавления не прошло валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            var response = await httpClient.PostAsJsonAsync(controllerUri, addDto);

            var result = await HttpResponseHandler.GetResult(response);

            return result;
        }

        public async Task<Result> DeleteImage(long id)
        {
            var response = await httpClient.DeleteAsync($"{controllerUri}/{id}");

            var result = await HttpResponseHandler.GetResult(response);

            return result;
        }

        public async Task<Result<ProductImage>> GetImageById(long imageId)
        {
            var response = await httpClient.GetAsync($"{controllerUri}/{imageId}");

            var result = await HttpResponseHandler.GetResult<ProductImage>(response);

            return result;
        }

        public async Task<Result<List<ProductImage>>> GetImagesByProductId(long productId)
        {
            var response = await httpClient.GetAsync($"{controllerUri}/Product/{productId}");

            var result = await HttpResponseHandler.GetResult<List<ProductImage>>(response);

            return result;
        }
    }
}
