using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductImageRepository
    {
        public Task<ErrorOr<Created>> InsertAsync(ProductImage image);
        
        public Task<ErrorOr<Deleted>> DeleteByIdAsync(long id);

        public Task<ErrorOr<ProductImage>> GetByIdAsync(long imageId);

        public Task<ErrorOr<List<ProductImage>>> GetAllByProductIdAsync(long productId);

        public Task<ErrorOr<ProductImage>> GetFirstByProductIdAsync(long productId);
    }
}
