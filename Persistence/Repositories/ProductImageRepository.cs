using Application.Common;
using Application.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal class ProductImageRepository(ApplicationDbContext context) : IProductImageRepository
    {
        public async Task<Result> AddImage(ProductImage image)
        {
            if (image is null)
                return new ErrorResult(message: "Невозможно добавить пустое изображение",
                                       errors: [ErrorList.IsNull]);

            await context.ProductImages.AddAsync(image);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result> DeleteImage(long id)
        {
            ProductImage? image = await context.ProductImages.FirstOrDefaultAsync(pi => pi.Id == id);

            if(image is null)
                return new NotFoundErrorResult(message: $"Изображение для удаления с id: {id} не найдено",
                                               errors: [ErrorList.NotFound]);

            context.ProductImages.Remove(image);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result<ProductImage>> GetImageById(long imageId)
        {
            ProductImage? image = await context.ProductImages.AsNoTracking()
                                                             .FirstOrDefaultAsync(pi => pi.Id == imageId);

            if (image is null)
                return new NotFoundErrorResult<ProductImage>(message: $"Изображение с id: {imageId} не найдено",
                                                             errors: [ErrorList.NotFound]);

            return new SuccessResult<ProductImage>(image);
        }

        public async Task<Result<List<ProductImage>>> GetImagesByProductId(long productId)
        {
            Product? product = await context.Products.AsNoTracking()
                                                     .Include(p => p.Images)
                                                     .FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null)
                return new NotFoundErrorResult<List<ProductImage>>(message: $"Продукт с id: {productId} не найден",
                                                                   errors: [ErrorList.NotFound]);

            return new SuccessResult<List<ProductImage>>(product.Images.ToList());
        }
    }
}
