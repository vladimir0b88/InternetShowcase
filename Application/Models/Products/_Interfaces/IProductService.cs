using Application.Common;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductService
    {
        Task<ErrorOr<Product>> GetProductById(long id);

        Task<ErrorOr<Deleted>> DeleteProductById(long id);

        Task<ErrorOr<Created>> AddProduct(ProductCreateDto productDto);

        Task<ErrorOr<List<Product>>> GetAllProducts();

        Task<ErrorOr<Updated>> UpdateProduct(ProductUpdateDto updateDto);

        Task<ErrorOr<List<Product>>> GetByProductTypeId(long productTypeId);

        Task<ErrorOr<FilteringResult<Product>>> GetProductsByFilter(ProductsFilter filter);
    }
}
