using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface ITypePropertyRepository
    {
        Task<ErrorOr<List<ProductTypeProperty>>> GetAllAsync();

        Task<ErrorOr<ProductTypeProperty>> GetByIdAsync(long propertyId);

        Task<ErrorOr<List<ProductTypeProperty>>> GetByTypeIdAsync(long typeId);

        Task<ErrorOr<Created>> InsertAsync(ProductTypeProperty property);

        Task<ErrorOr<Deleted>> DeleteAsync(long propertyId);

        Task<ErrorOr<Updated>> UpdateAsync(ProductTypeProperty property);

        Task<ErrorOr<Created>> AddPropertiesValuesForProductAsync(Product product);
    }
}
