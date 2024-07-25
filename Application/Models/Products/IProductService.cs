using Application.Models;
using Domain.Entities;

namespace Application.Common
{
    public interface IProductService
    {
        Task<Result<Product>> GetProductById(long id);

        Task<Result> DeleteProductById(long id);

        Task<Result> AddProduct(ProductCreateDto productDto);

        Task<Result<List<Product>>> GetAllProducts();

        Task<Result> UpdateProduct(ProductUpdateDto updateDto);

        Task<Result<List<Product>>> GetByProductTypeId(long productTypeId);

        Task<Result<List<Product>>> GetProductsByFilter(ProductsFilter filter);
    }
}
