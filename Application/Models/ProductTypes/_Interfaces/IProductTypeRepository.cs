using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductTypeRepository
    {
        Task<ErrorOr<List<ProductType>>> GetAllAsync();

        Task<ErrorOr<ProductType>> GetByIdAsync(long id);

        Task<ErrorOr<Created>> InsertAsync(ProductType productType);

        Task<ErrorOr<Deleted>> DeleteByIdAsync(long id);

        Task<ErrorOr<Updated>> UpdateAsync(ProductType productType);

        Task<bool> ExistByIdAsync(long id);
    }
}
