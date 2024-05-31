using Application.Models;
using Domain.Entities;

namespace Application.Common
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
