using Application.Common;
using Application.Models;
using Domain.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using static Application.Common.ProductsFilter;

namespace Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : IProductRepository
    {
        public async Task<ErrorOr<Product>> GetProductById(long id)
        {
            Product? product = await context.Products.AsNoTracking()
                                                     .Include(p => p.PropertyValues)
                                                     .ThenInclude(pv => pv.TypeProperty)
                                                     .Include(p => p.Type)
                                                     .Include(p => p.Images)
                                                     .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                return Error.NotFound(description: $"Продукт c id: {id} не был найден");

            return product;
        }


        public async Task<ErrorOr<Deleted>> DeleteProductById(long id)
        {
            Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                return Error.NotFound(description: $"Продукт для удаления с id: {id} не найден");


            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return Result.Deleted;
        }

        public async Task<ErrorOr<Created>> AddProduct(Product product)
        {
            if (product is null)
                return Error.Validation(description: "Нельзя добавить пустой продукт");


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

            return Result.Created;
        }

        public async Task<ErrorOr<List<Product>>> GetAll()
        {
            List<Product> list = await context.Products.AsNoTracking()
                                                       .Include(p => p.Type)
                                                       .ToListAsync();

            return list;
        }

        public async Task<ErrorOr<Updated>> UpdateProduct(Product product)
        {
            if (product is null)
                return Error.Validation(description: "Продукт для изменения не может быть пустым");


            Product? modifyingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            if (modifyingProduct is null)
                return Error.NotFound(description: $"Продукт для изменения с id: {product.Id} не был найден");

            modifyingProduct.Name = product.Name;
            modifyingProduct.Description = product.Description;
            modifyingProduct.Cost = product.Cost;
            modifyingProduct.TypeId = product.TypeId;

            context.Update(modifyingProduct);
            await context.SaveChangesAsync();

            return Result.Updated;
        }

        public async Task<ErrorOr<List<Product>>> GetByProductTypeId(long productTypeId)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == productTypeId);

            if (productType is null)
                return Error.NotFound(description: $"Не найден тип продукта с id: {productTypeId}");

            List<Product> list = await context.Products.AsNoTracking()
                                                       .Where(p => p.TypeId == productTypeId)
                                                       .ToListAsync();

            return list;
        }

        public async Task<ErrorOr<FilteringResult<Product>>> GetByFilter(ProductsFilter filter)
        {
            if (filter is null)
                return Error.Validation(description: "Фильтр не может быть пустым");


            IQueryable<Product> productQuery = context.Products.AsNoTracking()
                                                               .Include(p => p.Type)
                                                               .Include(p => p.PropertyValues);
            if (filter.ProductTypeId is not null)
            {
                ProductType? productType = await context.ProductTypes.AsNoTracking()
                                                                     .FirstOrDefaultAsync(pt => pt.Id == filter.ProductTypeId);

                if (productType is null)
                    return Error.Validation(description: "Указан несуществующий тип товара");

                productQuery = productQuery.Where(p => p.Type != null &&
                                                       p.Type.Id == filter.ProductTypeId);
            }

            if (filter.MinCost is not null)
                productQuery = productQuery.Where(p => p.Cost >= filter.MinCost);

            if (filter.MaxCost is not null)
                productQuery = productQuery.Where(p => p.Cost <= filter.MaxCost);


            if (filter.Name is not null)
            {
                productQuery = productQuery.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.PropertyFilters is not null)
                foreach (var propertyFilter in filter.PropertyFilters)
                {
                    productQuery = productQuery.Where(p => p.PropertyValues.Any(pv => propertyFilter.PropertyId == pv.PropertyId &&
                                                                                      propertyFilter.Values!.Contains(pv.Value!)));
                }

            productQuery = SortByMethod(productQuery, filter.SortingMethod);

            int totalItems = productQuery.Count();
            int totalPages = totalItems / filter.ItemsOnPage;

            if (totalItems > totalPages * filter.ItemsOnPage)
                totalPages++;

            if (totalPages < filter.PageNumber)
                filter.PageNumber = totalPages;



            List<Product> products = await productQuery.Skip(filter.ItemsOnPage * (filter.PageNumber - 1))
                                                       .Take(filter.ItemsOnPage)
                                                       .ToListAsync();

            FilteringResult<Product> result = new()
            {
                Result = products,
                SortingMethod = filter.SortingMethod,
                ItemsOnPage = filter.ItemsOnPage,
                CurrentPage = filter.PageNumber,
                TotalPages = totalPages,
                TotalItems = totalItems
            };

            return result;
        }
    }
}
