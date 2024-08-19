using Application.Models;
using Domain.Entities;

namespace Application.Common
{
    public interface IPropertyValueService
    {
        Task<Result<List<PropertyValue>>> GetAllPropertyValues();
        Task<Result<List<PropertyValue>>> GetPropertyValuesByProductId(long productId);
        Task<Result> UpdatePropertyValue(PropertyValueUpdateDto updateDto);

        Task<Result> UpdatePropertyValueList(PropertyValueUpdateDtoList updateDtoList);

        Task<Result<List<UniquePropertyValues>>> GetUniquePropertyValues(long productTypeId);
    }
}
