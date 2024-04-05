using Application.Common.Models;
using Application.Models.ProductTypes.Create;
using Application.Models.ProductTypes.Update;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IProductTypeService
    {
        public Task<Result<List<ProductType>>> GetAllProductTypes();

        public Task<Result<ProductType>> GetProductTypeById(long id);

        public Task<Result> AddProductType(ProductTypeCreateDto dto);

        public Task<Result> DeleteProductTypeById(long id);

        public Task<Result> UpdateProductType(ProductTypeUpdateDto dto);
    }
}
