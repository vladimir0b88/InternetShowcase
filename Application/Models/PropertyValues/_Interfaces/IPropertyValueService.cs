using Application.Common;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IPropertyValueService
    {
        Task<ErrorOr<List<PropertyValue>>> GetAllAsync();
        Task<ErrorOr<List<PropertyValue>>> GetByProductIdAsync(long productId);
        Task<ErrorOr<Updated>> UpdateAsync(PropertyValueUpdateDto updateDto);

        Task<ErrorOr<Updated>> UpdateListAsync(PropertyValueUpdateDtoList updateDtoList);

        Task<ErrorOr<List<UniquePropertyValues>>> GetUniquesByProductTypeIdAsync(long productTypeId);
    }
}
