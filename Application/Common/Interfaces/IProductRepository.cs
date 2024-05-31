using Domain.Entities;

namespace Application.Common
{
    public interface IProductRepository
    {
        Task<Result<List<Product>>> GetAll();
        Task<Result<Product>> GetProductById(long id);

        Task<Result> DeleteProductById(long id);

        Task<Result> AddProduct(Product product);

        Task<Result> UpdateProduct(Product product);

        Task<Result<List<Product>>> GetByProductTypeId(long productTypeId);
    }
}
