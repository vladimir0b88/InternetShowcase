using Application.Common;
using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IPropertyService
    {
        // Create
        Task<ErrorOr<Created>> AddPropertyAsync(TypePropertyAddDto createDto);
        Task<ErrorOr<Created>> AddPropertiesValuesForProductAsync(Product product);


        // Change
        Task<ErrorOr<Updated>> UpdatePropertyAsync(TypePropertyUpdateDto updateDto);

        Task<ErrorOr<Updated>> UpdatePropertyValueAsync(PropertyValueUpdateDto updateDto);

        Task<ErrorOr<Updated>> UpdatePropertyValuesListAsync(PropertyValueUpdateDtoList updateDtoList);
        

        // Delete
        Task<ErrorOr<Deleted>> DeletePropertyAsync(long propertyId);


        // Get
        Task<ErrorOr<List<ProductTypeProperty>>> GetAllPropertiesAsync();

        Task<ErrorOr<List<ProductTypeProperty>>> GetPropertyByProductTypeIdAsync(long typeId);

        Task<ErrorOr<ProductTypeProperty>> GetPropertyByIdAsync(long propertyId);

        Task<ErrorOr<List<PropertyValue>>> GetAllPropertyValuesAsync();

        Task<ErrorOr<List<PropertyValue>>> GetPropertyValuesByProductIdAsync(long productId);

        Task<ErrorOr<List<UniquePropertyValues>>> GetUniquesPropertyValuesByProductTypeIdAsync(long productTypeId);


    }
}
