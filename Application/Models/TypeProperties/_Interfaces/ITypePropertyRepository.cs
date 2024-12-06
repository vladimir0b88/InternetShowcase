using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface ITypePropertyRepository
    {
        Task<ErrorOr<List<TypeProperty>>> GetAllAsync();

        Task<ErrorOr<TypeProperty>> GetByIdAsync(long propertyId);

        Task<ErrorOr<List<TypeProperty>>> GetByTypeIdAsync(long typeId);

        Task<ErrorOr<Created>> InsertAsync(TypeProperty property);

        Task<ErrorOr<Deleted>> DeleteAsync(long propertyId);

        Task<ErrorOr<Updated>> UpdateAsync(TypeProperty property);
    }
}
