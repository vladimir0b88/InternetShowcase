using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface ITypePropertyService
    {
        Task<ErrorOr<List<TypeProperty>>> GetAllAsync();

        Task<ErrorOr<List<TypeProperty>>> GetByProductTypeIdAsync(long typeId);

        Task<ErrorOr<TypeProperty>> GetByIdAsync(long propertyId);

        Task<ErrorOr<Created>> AddAsync(TypePropertyAddDto createDto);

        Task<ErrorOr<Deleted>> DeleteAsync(long propertyId);

        Task<ErrorOr<Updated>> UpdateAsync(TypePropertyUpdateDto updateDto);
    }
}
