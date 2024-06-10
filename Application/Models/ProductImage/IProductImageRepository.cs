using Application.Common;
using Domain.Entities;

namespace Application.Models
{
    public interface IProductImageRepository
    {
        public Task<Result> AddImage(ProductImage image);
        
        public Task<Result> DeleteImage(long id);

        public Task<Result<ProductImage>> GetImageById(long imageId);

        public Task<Result<List<ProductImage>>> GetImagesByProductId(long productId);
    }
}
