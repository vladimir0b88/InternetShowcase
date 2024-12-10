using Application.Common;
using Application.Models;
using Domain.Entities;
using ErrorOr;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Common
{
    public class ProductImageHttpService(IHttpClientFactory httpClientFactory,
                                         IValidator<ProductImageAddDto> addValidator) : IProductImageService
    {
        private readonly HttpClient httpClient = httpClientFactory.CreateClient("WebApi");

        private const string controllerUri = "api/ProductImages";

        public async Task<ErrorOr<Created>> AddAsync(ProductImageAddDto addDto)
        {
            var validationResult = await addValidator.ValidateAsync(addDto);

            if (!validationResult.IsValid)
                return validationResult.GetGeneralError();

            var response = await httpClient.PostAsJsonAsync(controllerUri, addDto);

            var result = await HttpResponseHandler.GetResultAsync<Created>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(long imageId)
        {
            var response = await httpClient.DeleteAsync($"{controllerUri}/{imageId}");

            var result = await HttpResponseHandler.GetResultAsync<Deleted>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<ProductImage>> GetByIdAsync(long imageId)
        {
            var response = await httpClient.GetAsync($"{controllerUri}/{imageId}");

            var result = await HttpResponseHandler.GetResultAsync<ProductImage>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<List<ProductImage>>> GetAllByProductIdAsync(long productId)
        {
            var response = await httpClient.GetAsync($"{controllerUri}/Product/{productId}");

            var result = await HttpResponseHandler.GetResultAsync<List<ProductImage>>(response);

            await Task.Delay(Constant.ServiceDelay);

            return result;
        }

        public async Task<ErrorOr<ProductImage>> GetFirstByProductIdAsync(long productId)
        {
            var response = await httpClient.GetAsync($"{controllerUri}/Product/{productId}/First");

            var result = await HttpResponseHandler.GetResultAsync<ProductImage>(response);

            //var rnd = new Random();
            //await Task.Delay(Constant.ServiceDelay + rnd.Next(100,2000));

            return result;
        }
    }
}
