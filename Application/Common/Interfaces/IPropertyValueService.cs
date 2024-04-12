using Application.Common.Models;
using Application.Models.PropertyValues.Update;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IPropertyValueService
    {
        Task<Result<List<PropertyValue>>> GetPropertyValuesByProductId(long productId);
        Task<Result> UpdatePropertyValue(PropertyValueUpdateDto updateDto);
    }
}
