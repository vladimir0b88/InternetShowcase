using Application.Common;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductRepository
    {
        Task<ErrorOr<List<Product>>> GetAll();
        Task<ErrorOr<Product>> GetProductById(long id);

        Task<ErrorOr<Deleted>> DeleteProductById(long id);

        Task<ErrorOr<Created>> AddProduct(Product product);

        Task<ErrorOr<Updated>> UpdateProduct(Product product);

        Task<ErrorOr<List<Product>>> GetByProductTypeId(long productTypeId);

        Task<ErrorOr<FilteringResult<Product>>> GetByFilter(ProductsFilter filter);
    }
}
