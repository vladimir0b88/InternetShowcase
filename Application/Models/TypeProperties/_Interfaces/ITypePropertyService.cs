using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface ITypePropertyService
    {
        Task<ErrorOr<List<TypeProperty>>> GetAllTypeProperties();

        Task<ErrorOr<List<TypeProperty>>> GetPropertiesByProductTypeId(long typeId);

        Task<ErrorOr<TypeProperty>> GetPropertyById(long propertyId);

        Task<ErrorOr<Created>> AddProperty(TypePropertyCreateDto createDto);

        Task<ErrorOr<Deleted>> DeleteProperty(long propertyId);

        Task<ErrorOr<Updated>> UpdateProperty(TypePropertyUpdateDto updateDto);
    }
}
