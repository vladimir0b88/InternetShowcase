using Application;
using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductTypeRepository(ApplicationDbContext context) : IProductTypeRepository
    {
        public async Task<Result<List<ProductType>>> GetAllProductTypes()
        {
            List<ProductType> productTypes = await context.ProductTypes.AsNoTracking().ToListAsync();

            return new SuccessResult<List<ProductType>>(productTypes);
        }
        
        public async Task<Result<ProductType>> GetProductTypeById(long id)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking()
                                                                 .FirstOrDefaultAsync(pt => pt.Id == id);

            if(productType is null)
                return new NotFoundErrorResult<ProductType>(message: $"Тип товара с id: {id} не найден",
                                                            errors: [ErrorList.NotFound]);

            return new SuccessResult<ProductType>(productType);
        }

        public async Task<Result> AddProductType(ProductType newProductType)
        {
            if (newProductType is null)
                return new ErrorResult(message: "Тип продукта не может быть пустым",
                                       errors: [ErrorList.IsNull]);

            await context.ProductTypes.AddAsync(newProductType);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result> DeleteProductTypeById(long id)
        {
            ProductType? productType = await context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == id);

            if(productType is null)
                return new NotFoundErrorResult(message: $"Тип продукта с id: {id} не найден",
                                               errors: [ErrorList.NotFound]);

            context.ProductTypes.Remove(productType);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result> UpdateProductType(ProductType productType)
        {
            if(productType is null)
                return new ErrorResult(message: "Тип продукта не может быть пустым",
                                       errors: [ErrorList.IsNull]);

            ProductType? modifyingProductType = await context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == productType.Id);

            if (modifyingProductType is null)
                return new NotFoundErrorResult(message: $"Тип продукта с id: {productType.Id} не найден",
                                               errors: [ErrorList.NotFound]);

            modifyingProductType.Name = productType.Name;

            context.Update(modifyingProductType);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<bool> ExistById(long id)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking()
                                                                 .FirstOrDefaultAsync(pt => pt.Id == id);
            return productType is not null;
        }
    }
}
