using Domain.Entities;
using ErrorOr;

namespace Application.Models
{
    public interface IProductImageRepository
    {
        public Task<ErrorOr<Created>> AddImage(ProductImage image);
        
        public Task<ErrorOr<Deleted>> DeleteImage(long id);

        public Task<ErrorOr<ProductImage>> GetImageById(long imageId);

        public Task<ErrorOr<List<ProductImage>>> GetImagesByProductId(long productId);

        public Task<ErrorOr<ProductImage>> GetFirstImageByProductId(long productId);
    }
}
