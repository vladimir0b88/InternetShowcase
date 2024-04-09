using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.Products.Create;
using Application.Models.Products.Update;
using Domain.Entities;

namespace BlazorWebAssembly.Services
{
    public class ClientProductService : IProductService
    {
        public Task<Result> AddProduct(ProductCreateDto productDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteProductById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Product>>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Product>>> GetByProductTypeId(long productTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Product>> GetProductById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateProduct(ProductUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
