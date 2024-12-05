using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface ITypePropertyRepository
    {
        Task<ErrorOr<List<TypeProperty>>> GetAllTypeProperties();

        Task<ErrorOr<TypeProperty>> GetPropertyById(long propertyId);

        Task<ErrorOr<List<TypeProperty>>> GetPropertiesByTypeId(long typeId);

        Task<ErrorOr<Created>> AddProperty(TypeProperty property);

        Task<ErrorOr<Deleted>> DeleteProperty(long propertyId);

        Task<ErrorOr<Updated>> UpdateProperty(TypeProperty property);
    }
}
