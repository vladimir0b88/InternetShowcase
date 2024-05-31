using Application.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : IProductRepository
    {
        public async Task<Result<Product>> GetProductById(long id)
        {
            Product? product = await context.Products.AsNoTracking()
                                                     .Include(p => p.PropertyValues)
                                                     .ThenInclude(pv => pv.TypeProperty)
                                                     .Include(p => p.Type)
                                                     .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                return new NotFoundErrorResult<Product>(message: $"Продукт c id: {id} не был найден",
                                                        errors: [ErrorList.NotFound]);

            return new SuccessResult<Product>(product);
        }


        public async Task<Result> DeleteProductById(long id)
        {
            Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                return new NotFoundErrorResult(message: $"Продукт для удаления с id: {id} не найден",
                                               errors: [ErrorList.NotFound]);


            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result> AddProduct(Product product)
        {
            if (product is null)
                return new ErrorResult(message:"Нельзя добавить пустой продукт",
                                        errors: [ErrorList.IsNull]);


            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            if(product.TypeId is not null)
            {
                List<long> propertiesId = await context.TypeProperties.AsNoTracking()
                                                                      .Where(tp => tp.TypeId == product.TypeId)
                                                                      .Select(tp => tp.Id)
                                                                      .ToListAsync();

                foreach(long id in propertiesId)
                {
                    await context.PropertyValues.AddAsync(new PropertyValue()
                    {
                        ProductId = product.Id,
                        PropertyId = id,
                        Value = ""
                    });
                }
            }
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result<List<Product>>> GetAll()
        {
            List<Product> list = await context.Products.AsNoTracking()
                                                       .ToListAsync();

            return new SuccessResult<List<Product>>(list);
        }

        public async Task<Result> UpdateProduct(Product product)
        {
            if (product is null)
                return new ErrorResult(message: "Продукт для изменения не может быть пустым",
                                       errors: [ErrorList.IsNull]);


            Product? modifyingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            if(modifyingProduct is null)
                return new NotFoundErrorResult(message: $"Продукт для изменения с id: {product.Id} не был найден",
                                               errors: [ErrorList.NotFound]);
            
            modifyingProduct.Name = product.Name;
            modifyingProduct.Description = product.Description;
            modifyingProduct.Cost = product.Cost;
            modifyingProduct.TypeId = product.TypeId;

            context.Update(modifyingProduct);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result<List<Product>>> GetByProductTypeId(long productTypeId)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == productTypeId);

            if (productType is null)
                return new NotFoundErrorResult<List<Product>>(message: $"Не найден тип продукта с id: {productTypeId}",
                                                              errors: [ErrorList.NotFound]);

            List<Product> list = await context.Products.AsNoTracking()
                                                       .Where(p => p.TypeId == productTypeId)
                                                       .ToListAsync();

            return new SuccessResult<List<Product>>(list);
        }
    }
}
