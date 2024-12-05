using Application.Common;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IPropertyValueService
    {
        Task<ErrorOr<List<PropertyValue>>> GetAllPropertyValues();
        Task<ErrorOr<List<PropertyValue>>> GetPropertyValuesByProductId(long productId);
        Task<ErrorOr<Updated>> UpdatePropertyValue(PropertyValueUpdateDto updateDto);

        Task<ErrorOr<Updated>> UpdatePropertyValueList(PropertyValueUpdateDtoList updateDtoList);

        Task<ErrorOr<List<UniquePropertyValues>>> GetUniquePropertyValues(long productTypeId);
    }
}
