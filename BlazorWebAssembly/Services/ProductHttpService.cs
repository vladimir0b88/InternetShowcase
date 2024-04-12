using Application;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.Products.Create;
using Application.Models.Products.Update;
using Domain.Entities;
using System.Net;
using System.Net.Http.Json;

namespace BlazorWebAssembly.Services
{
    public class ProductHttpService(HttpClient httpClient) : IProductService
    {
        private const string _controllerUri = "api/Products";

        public async Task<Result> AddProduct(ProductCreateDto productDto)
        {
            var response = await httpClient.PostAsJsonAsync(_controllerUri, productDto);

            var result = await response.Content.ReadFromJsonAsync<Result>();

            if (result is null)
                return new ErrorResult(message: "Ответ от сервера не получен",
                                       errors: [ErrorList.ServerUnavailable]);

            return result;
        }

        public Task<Result> DeleteProductById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Product>>> GetAllProducts()
        {
            var response = await httpClient.GetAsync(_controllerUri);

            Result<List<Product>>? result;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    result = await response.Content.ReadFromJsonAsync<SuccessResult<List<Product>>>();
                    break;

                case HttpStatusCode.BadRequest:
                    result = await response.Content.ReadFromJsonAsync<ErrorResult<List<Product>>>();
                    break;

                default: throw new NotImplementedException();
            }

            return result!;
        }

        public Task<Result<List<Product>>> GetByProductTypeId(long productTypeId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Product>> GetProductById(long id)
        {
            var response = await httpClient.GetAsync(_controllerUri + $"/{id}");

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<Product>();

                return new SuccessResult<Product>(new Product() { Name = product.Name });
            }


            return new ErrorResult<Product>("1");
        }

        public Task<Result> UpdateProduct(ProductUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
