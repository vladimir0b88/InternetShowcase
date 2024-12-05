using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductTypeRepository
    {
        Task<ErrorOr<List<ProductType>>> GetAllProductTypes();

        Task<ErrorOr<ProductType>> GetProductTypeById(long id);

        Task<ErrorOr<Created>> AddProductType(ProductType newProductType);

        Task<ErrorOr<Deleted>> DeleteProductTypeById(long id);

        Task<ErrorOr<Updated>> UpdateProductType(ProductType productType);

        Task<bool> ExistById(long id);
    }
}
