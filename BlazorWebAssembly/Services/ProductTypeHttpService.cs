using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Models.ProductTypes.Create;
using Application.Models.ProductTypes.Update;
using Domain.Entities;

namespace BlazorWebAssembly.Services
{
    public class ProductTypeHttpService : IProductTypeService
    {
        public Task<Result<List<ProductType>>> GetAllProductTypes()
        {
            throw new NotImplementedException();
        }

        public Task<Result<ProductType>> GetProductTypeById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> AddProductType(ProductTypeCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateProductType(ProductTypeUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteProductTypeById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
