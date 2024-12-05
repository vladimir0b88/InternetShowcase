using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductTypeService
    {
        public Task<ErrorOr<List<ProductType>>> GetAllProductTypes();

        public Task<ErrorOr<ProductType>> GetProductTypeById(long id);

        public Task<ErrorOr<Created>> AddProductType(ProductTypeCreateDto dto);

        public Task<ErrorOr<Deleted>> DeleteProductTypeById(long id);

        public Task<ErrorOr<Updated>> UpdateProductType(ProductTypeUpdateDto dto);
    }
}
