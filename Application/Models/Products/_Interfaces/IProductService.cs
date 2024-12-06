using Application.Common;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductService
    {
        Task<ErrorOr<Product>> GetByIdAsync(long id);

        Task<ErrorOr<Deleted>> DeleteByIdAsync(long id);

        Task<ErrorOr<Created>> AddAsync(ProductAddDto productDto);

        Task<ErrorOr<List<Product>>> GetAllAsync();

        Task<ErrorOr<Updated>> UpdateAsync(ProductUpdateDto updateDto);

        Task<ErrorOr<List<Product>>> GetByProductTypeIdAsync(long productTypeId);

        Task<ErrorOr<FilteringResult<Product>>> GetByFilterAsync(ProductsFilter filter);
    }
}
