
using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IPropertyValueRepository
    {
        Task<Result<List<PropertyValue>>> GetPropertyValuesByProductId(long productId);

        Task<Result> AddPropertyValue(PropertyValue propertyValue);
        Task<Result> UpdatePropertyValue(PropertyValue propertyValue);
        Task<Result> DeletePropertyValueById(long propertyValueId);
    }
}
