using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductTypeService
    {
        public Task<ErrorOr<List<ProductType>>> GetAllAsync();

        public Task<ErrorOr<ProductType>> GetByIdAsync(long id);

        public Task<ErrorOr<Created>> AddAsync(ProductTypeAddDto dto);

        public Task<ErrorOr<Deleted>> DeleteByIdAsync(long id);

        public Task<ErrorOr<Updated>> UpdateAsync(ProductTypeUpdateDto dto);
    }
}
