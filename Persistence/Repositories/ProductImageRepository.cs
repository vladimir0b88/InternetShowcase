using Application.Common;
using Application.Models;
using Domain.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Persistence.Repositories
{
    internal class ProductImageRepository(ApplicationDbContext context) : IProductImageRepository
    {
        public async Task<ErrorOr<Created>> InsertAsync(ProductImage image)
        {
            if (image is null)
                return Error.Validation(description: "Невозможно добавить пустое изображение");

            await context.ProductImages.AddAsync(image);
            await context.SaveChangesAsync();

            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(long id)
        {
            ProductImage? image = await context.ProductImages.FirstOrDefaultAsync(pi => pi.Id == id);

            if (image is null)
                return Error.NotFound(description: $"Изображение для удаления с id: {id} не найдено");

            context.ProductImages.Remove(image);
            await context.SaveChangesAsync();

            return Result.Deleted;
        }

        public async Task<ErrorOr<ProductImage>> GetFirstByProductIdAsync(long productId)
        {
            Product? product = await context.Products.AsNoTracking()
                                                     .Include(p => p.Images)
                                                     .FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null)
                return Error.NotFound(description: $"Продукт с id: {productId} не найден");

            ProductImage? productImage = await context.ProductImages.AsNoTracking()
                                                                    .FirstOrDefaultAsync(pi => pi.ProductId == productId);

            if (productImage is null)
                return Error.NotFound(description: $"У продукта с id: {productId} отсутствуют изображения");

            return productImage;
        }

        public async Task<ErrorOr<ProductImage>> GetByIdAsync(long imageId)
        {
            ProductImage? image = await context.ProductImages.AsNoTracking()
                                                             .FirstOrDefaultAsync(pi => pi.Id == imageId);

            if (image is null) 
                return Error.NotFound(description: $"Изображение с id: {imageId} не найдено");

            return image;
        }

        public async Task<ErrorOr<List<ProductImage>>> GetAllByProductIdAsync(long productId)
        {
            Product? product = await context.Products.AsNoTracking()
                                                     .Include(p => p.Images)
                                                     .FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null)
                return Error.NotFound(description: $"Продукт с id: {productId} не найден");

            return product.Images.ToList();
        }
    }
}
