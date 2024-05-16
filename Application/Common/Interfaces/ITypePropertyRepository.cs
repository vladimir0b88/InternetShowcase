using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITypePropertyRepository
    {
        Task<Result<List<TypeProperty>>> GetAllTypeProperties();

        Task<Result<TypeProperty>> GetPropertyById(long propertyId);

        Task<Result<List<TypeProperty>>> GetPropertiesByTypeId(long typeId);

        Task<Result> AddProperty(TypeProperty property);

        Task<Result> DeleteProperty(long propertyId);

        Task<Result> UpdateProperty(TypeProperty property);
    }
}
