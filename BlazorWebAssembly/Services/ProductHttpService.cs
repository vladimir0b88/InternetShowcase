using Application;
using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.Products.Create;
using Application.Models.Products.Update;
using BlazorWebAssembly.Common;
using Domain.Entities;
using FluentValidation;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class ProductHttpService(HttpClient httpClient,
                                    IValidator<ProductCreateDto> createValidator,
                                    IValidator<ProductUpdateDto> updateValidator) : IProductService
    {
        private const string _controllerUri = "api/Products";

        public async Task<Result<List<Product>>> GetAllProducts()
        {
            Result<List<Product>> result;
            try
            {
                var response = await httpClient.GetAsync(_controllerUri);

                result = await HttpResponseHandler.GetResult<List<Product>>(response);
            }
            catch
            {
                result = new ErrorResult<List<Product>>(message: "API не отвечает",
                                                        errors: [ErrorList.ServerUnavailable]);
            }

            return result;
        }

        public async Task<Result<List<Product>>> GetByProductTypeId(long productTypeId)
        {
            Result<List<Product>> result;
            try
            {
                var response = await httpClient.GetAsync($"{_controllerUri}/ProductType/{productTypeId}");

                result = await HttpResponseHandler.GetResult<List<Product>>(response);
            }
            catch
            {
                result = new ErrorResult<List<Product>>(message: "API не отвечает",
                                                        errors: [ErrorList.ServerUnavailable]);
            }

            return result;
        }

        public async Task<Result<Product>> GetProductById(long id)
        {
            Result<Product> result;
            try
            {
                var response = await httpClient.GetAsync($"{_controllerUri}/{id}");

                result = await HttpResponseHandler.GetResult<Product>(response);
            }
            catch
            {
                result = new ErrorResult<Product>(message: "API не отвечает",
                                                  errors: [ErrorList.ServerUnavailable]);
            }

            return result;
        }

        public async Task<Result> AddProduct(ProductCreateDto productDto)
        {
            var validationResult = await createValidator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Продукт для создания не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);
            
            Result result;

            try
            {
                var response = await httpClient.PostAsJsonAsync(_controllerUri, productDto);

                result = await HttpResponseHandler.GetResult(response);
            }
            catch
            {
                result = new ErrorResult(message: "API не отвечает",
                                         errors: [ErrorList.ServerUnavailable]);
            }

            return result;
        }

        public async Task<Result> UpdateProduct(ProductUpdateDto updateDto)
        {
            var validationResult = await updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
                return new ValidationErrorResult(message: "Продукт для модификации не прошел валидацию",
                                                 errors: [ErrorList.FailedValidation],
                                                 validationErrors: validationResult.Errors);

            Result result;

            try
            {
                var response = await httpClient.PutAsJsonAsync(_controllerUri, updateDto);

                result = await HttpResponseHandler.GetResult(response);
            }
            catch
            {
                result = new ErrorResult(message: "API не отвечает",
                                         errors: [ErrorList.ServerUnavailable]);
            }

            return result;
        }

        public async Task<Result> DeleteProductById(long id)
        {
            Result result;

            try
            {
                var response = await httpClient.DeleteAsync($"{_controllerUri}/{id}");

                result = await HttpResponseHandler.GetResult(response);
            }
            catch
            {
                result = new ErrorResult(message: "API не отвечает",
                                         errors: [ErrorList.ServerUnavailable]);
            }

            return result;
        }
    }
}
