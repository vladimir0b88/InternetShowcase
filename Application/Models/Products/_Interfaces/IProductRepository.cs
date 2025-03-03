using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductRepository
    {
        Task<ErrorOr<List<Product>>> GetAllAsync();

        Task<ErrorOr<Product>> GetByIdAsync(long id);

        Task<ErrorOr<Deleted>> DeleteByIdAsync(long id);

        Task<ErrorOr<Product>> InsertAsync(Product product);

        Task<ErrorOr<Updated>> UpdateAsync(Product product);

        Task<ErrorOr<List<Product>>> GetByProductTypeIdAsync(long productTypeId);

        Task<ErrorOr<ProductsFilteringResult>> GetByFilterAsync(ProductsFilter filter);
    }
}
