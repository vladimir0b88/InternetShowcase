using Domain.Entities;

namespace Application.Common
{
    public interface IProductTypeRepository
    {
        Task<Result<List<ProductType>>> GetAllProductTypes();

        Task<Result<ProductType>> GetProductTypeById(long id);

        Task<Result> AddProductType(ProductType newProductType);

        Task<Result> DeleteProductTypeById(long id);

        Task<Result> UpdateProductType(ProductType productType);

        Task<bool> ExistById(long id);
    }
}
