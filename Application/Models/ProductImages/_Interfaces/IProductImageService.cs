using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductImageService
    {
        public Task<ErrorOr<Created>> AddAsync(ProductImageAddDto addDto);

        public Task<ErrorOr<Deleted>> DeleteByIdAsync(long imageId);

        public Task<ErrorOr<ProductImage>> GetByIdAsync(long imageId);

        public Task<ErrorOr<List<ProductImage>>> GetAllByProductIdAsync(long productId);

        public Task<ErrorOr<ProductImage>> GetFirstByProductIdAsync(long productId);

    }
}
