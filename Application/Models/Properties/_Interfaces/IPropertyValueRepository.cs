using Application.Common;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IPropertyValueRepository
    {
        Task<ErrorOr<List<PropertyValue>>> GetAllAsync();
        Task<ErrorOr<List<PropertyValue>>> GetByProductIdAsync(long productId);
        Task<ErrorOr<Created>> InsertAsync(PropertyValue propertyValue);
        Task<ErrorOr<Updated>> UpdateAsync(PropertyValue propertyValue);
        Task<ErrorOr<Deleted>> DeleteByIdAsync(long propertyValueId);
        Task<ErrorOr<List<UniquePropertyValues>>> GetUniquesByProductTypeIdAsync(long productTypeId);
    }
}
