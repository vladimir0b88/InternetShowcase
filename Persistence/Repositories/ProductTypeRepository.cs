using Application.Models;
using Domain.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductTypeRepository(ApplicationDbContext context) : IProductTypeRepository
    {
        public async Task<ErrorOr<List<ProductType>>> GetAllAsync()
        {
            List<ProductType> productTypes = await context.ProductTypes.AsNoTracking()
                                                                       .ToListAsync();

            return productTypes;
        }
        
        public async Task<ErrorOr<ProductType>> GetByIdAsync(long id)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking()
                                                                 .Include(pt => pt.Products)
                                                                 .Include(pt => pt.Properties)
                                                                 .FirstOrDefaultAsync(pt => pt.Id == id);

            if (productType is null)
                return Error.NotFound(description: $"Тип товара с id: {id} не найден");

            return productType;
        }

        public async Task<ErrorOr<Created>> InsertAsync(ProductType productType)
        {
            if (productType is null)
                return Error.Validation(description: "Тип продукта не может быть пустым");

            await context.ProductTypes.AddAsync(productType);
            await context.SaveChangesAsync();

            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(long id)
        {
            ProductType? productType = await context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);

            if (productType is null)
                return Error.NotFound(description: $"Тип продукта с id: {id} не найден");

            context.ProductTypes.Remove(productType);
            await context.SaveChangesAsync();

            return Result.Deleted;
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(ProductType productType)
        {
            if (productType is null)
                return Error.Validation(description: "Тип продукта не может быть пустым");

            ProductType? modifyingProductType = await context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == productType.Id);

            if (modifyingProductType is null)
                return Error.NotFound(description: $"Тип продукта с id: {productType.Id} не найден");

            modifyingProductType.Name = productType.Name;

            context.Update(modifyingProductType);
            await context.SaveChangesAsync();

            return Result.Updated;
        }

        public async Task<bool> ExistByIdAsync(long id)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking()
                                                                 .FirstOrDefaultAsync(pt => pt.Id == id);
            return productType is not null;
        }
    }
}
