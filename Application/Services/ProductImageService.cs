using Application.Common;
using Application.Models;
using Domain.Entities;

namespace Application.Services
{
    public class ProductImageService(IProductImageRepository repository) : IProductImageService
    {
        public Task<Result> AddImage(ProductImageAddDto addDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> DeleteImage(long id)
        {
            var result = await repository.DeleteImage(id);

            return result;
        }

        public async Task<Result<ProductImage>> GetImageById(long imageId)
        {
            var result = await repository.GetImageById(imageId);

            return result;
        }

        public async Task<Result<List<ProductImage>>> GetImagesByProductId(long productId)
        {
            var result = await repository.GetImagesByProductId(productId);

            return result;
        }
    }
}
