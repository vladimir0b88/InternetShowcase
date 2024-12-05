using Application.Common;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IPropertyValueRepository
    {
        Task<ErrorOr<List<PropertyValue>>> GetAllPropertyValues();
        Task<ErrorOr<List<PropertyValue>>> GetPropertyValuesByProductId(long productId);
        Task<ErrorOr<Created>> AddPropertyValue(PropertyValue propertyValue);
        Task<ErrorOr<Updated>> UpdatePropertyValue(PropertyValue propertyValue);
        Task<ErrorOr<Deleted>> DeletePropertyValueById(long propertyValueId);
        Task<ErrorOr<List<UniquePropertyValues>>> GetUniquePropertyValues(long productTypeId);
    }
}
