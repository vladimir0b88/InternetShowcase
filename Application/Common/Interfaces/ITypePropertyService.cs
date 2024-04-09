using Application.Common.Models;
using Application.Models.TypeProperties.Create;
using Application.Models.TypeProperties.Update;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITypePropertyService
    {
        Task<Result<List<TypeProperty>>> GetPropertiesByProductTypeId(long typeId);

        Task<Result> AddProperty(TypePropertyCreateDto createDto);

        Task<Result> DeleteProperty(long propertyId);

        Task<Result> UpdateProperty(TypePropertyUpdateDto updateDto);
    }
}
