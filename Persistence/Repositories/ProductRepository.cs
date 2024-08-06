using Application.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Application.Common.ProductsFilter;

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
                                                     .Include(p => p.Images)
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
                return new ErrorResult(message: "Нельзя добавить пустой продукт",
                                        errors: [ErrorList.IsNull]);


            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            if (product.TypeId is not null)
            {
                List<long> propertiesId = await context.TypeProperties.AsNoTracking()
                                                                      .Where(tp => tp.TypeId == product.TypeId)
                                                                      .Select(tp => tp.Id)
                                                                      .ToListAsync();

                foreach (long id in propertiesId)
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
                                                       .Include(p => p.Type)
                                                       .ToListAsync();

            return new SuccessResult<List<Product>>(list);
        }

        public async Task<Result> UpdateProduct(Product product)
        {
            if (product is null)
                return new ErrorResult(message: "Продукт для изменения не может быть пустым",
                                       errors: [ErrorList.IsNull]);


            Product? modifyingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            if (modifyingProduct is null)
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

        public async Task<Result<FilteringResult<Product>>> GetByFilter(ProductsFilter filter)
        {
            if (filter is null)
                return new ErrorResult<FilteringResult<Product>>(message: "Фильтр не может быть пустым",
                                                      errors: [ErrorList.IsNull]);


            IQueryable<Product> productQuery = context.Products.AsNoTracking()
                                                               .Include(p => p.Type)
                                                               .Include(p => p.PropertyValues);
            if (filter.ProductTypeId is not null)
            {
                productQuery = productQuery.Where(p => p.Type != null &&
                                                       p.Type.Id == filter.ProductTypeId);
            }

            if (filter.PropertyFilters is not null)
                foreach (var propertyFilter in filter.PropertyFilters)
                {
                    productQuery = productQuery.Where(p => p.PropertyValues.Any(pv => propertyFilter.PropertyId == pv.PropertyId &&
                                                                                      propertyFilter.Values!.Contains(pv.Value!)));
                }

            switch (filter.SortingMethod)
            {
                case SortingMethods.ByNameDesk:
                    productQuery = productQuery.OrderByDescending(p => p.Name);
                    break;
                case SortingMethods.ByNameAsk:
                    productQuery = productQuery.OrderBy(p => p.Name);
                    break;

                case SortingMethods.ByCostDesk:
                    productQuery = productQuery.OrderByDescending(p => p.Cost);
                    break;
                case SortingMethods.ByCostAsk:
                    productQuery = productQuery.OrderBy(p => p.Cost);
                    break;
            }


            int totalItems = productQuery.Count();
            int totalPages = totalItems / filter.ItemsOnPage;

            if (totalPages * filter.ItemsOnPage < totalItems)
                totalPages++;

            if(totalPages < filter.PageNumber)
                filter.PageNumber = totalPages;

            List<Product> products = new();
            FilteringResult<Product> result = new();

            if (totalItems < filter.ItemsOnPage * filter.PageNumber)
            {
                products = await productQuery.TakeLast(filter.ItemsOnPage)
                                             .ToListAsync();

                result = new FilteringResult<Product>()
                {
                    Results = products,
                    SortingMethod = filter.SortingMethod,
                    ItemsOnPage = filter.ItemsOnPage,
                    CurrentPage = totalPages,
                    TotalPages = totalPages,
                    TotalItems = totalItems
                };
            }
            else
            {
                products = await productQuery.Skip(filter.ItemsOnPage * (filter.PageNumber - 1))
                                             .Take(filter.ItemsOnPage)
                                             .ToListAsync();

                result = new FilteringResult<Product>()
                {
                    Results = products,
                    SortingMethod = filter.SortingMethod,
                    ItemsOnPage = filter.ItemsOnPage,
                    CurrentPage = filter.PageNumber,
                    TotalPages = totalPages,
                    TotalItems = totalItems
                };
            }

            return new SuccessResult<FilteringResult<Product>>(result);
        }
    }
}
