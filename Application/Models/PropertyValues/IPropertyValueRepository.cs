using Domain.Entities;

namespace Application.Common
{
    public interface IPropertyValueRepository
    {
        Task<Result<List<PropertyValue>>> GetAllPropertyValues();
        Task<Result<List<PropertyValue>>> GetPropertyValuesByProductId(long productId);
        Task<Result> AddPropertyValue(PropertyValue propertyValue);
        Task<Result> UpdatePropertyValue(PropertyValue propertyValue);
        Task<Result> DeletePropertyValueById(long propertyValueId);
        Task<Result<List<UniquePropertyValues>>> GetUniquePropertyValues(long productTypeId);
    }
}
