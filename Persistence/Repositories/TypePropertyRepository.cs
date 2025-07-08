using Application.Models;
using Domain.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class TypePropertyRepository(ApplicationDbContext context) : ITypePropertyRepository
    {
        public async Task<ErrorOr<Created>> InsertAsync(ProductTypeProperty property)
        {
            if (property is null)
                return Error.Validation(description: "Свойство типа продукта не может быть пустым");

            await context.TypeProperties.AddAsync(property);
            await context.SaveChangesAsync();


            List<long> productsId = await context.Products.AsNoTracking()
                                                           .Where(p => p.TypeId == property.TypeId)
                                                           .Select(p => p.Id)
                                                           .ToListAsync();

            foreach(long id in productsId)
            {
                await context.PropertyValues.AddAsync(new PropertyValue {
                    ProductId = id,
                    PropertyId = property.Id,
                    Value = ""
                });
            }
            await context.SaveChangesAsync();

            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> DeleteAsync(long id)
        {
            ProductTypeProperty? property = await context.TypeProperties.FirstOrDefaultAsync(p => p.Id == id);

            if (property is null)
                return Error.NotFound(description: $"Свойство типа продукта с id: {id} не найдено");

            context.TypeProperties.Remove(property);
            await context.SaveChangesAsync();

            return Result.Deleted;
        }

        public async Task<ErrorOr<List<ProductTypeProperty>>> GetAllAsync()
        {
            List<ProductTypeProperty> list = await context.TypeProperties.AsNoTracking()
                                                                  .ToListAsync();

            return list;
        }

        public async Task<ErrorOr<List<ProductTypeProperty>>> GetByTypeIdAsync(long productTypeId)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking()
                                                                 .FirstOrDefaultAsync(pt => pt.Id == productTypeId);

            if (productType is null)
                return Error.NotFound(description: $"Не найден тип продукта с id: {productTypeId}");

            List<ProductTypeProperty> list = await context.TypeProperties.AsNoTracking()
                                                                  .Where(p => p.TypeId == productTypeId)
                                                                  .ToListAsync();

            return list;
        }

        public async Task<ErrorOr<ProductTypeProperty>> GetByIdAsync(long propertyId)
        {
            ProductTypeProperty? typeProperty = await context.TypeProperties.AsNoTracking()
                                                                     .FirstOrDefaultAsync (tp => tp.Id == propertyId);

            if(typeProperty is null)
                return Error.NotFound(description: $"Свойство с id: {propertyId} не найден");

            return typeProperty;
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(ProductTypeProperty property)
        {
            if(property is null)
                return Error.Validation(description: "Свойство типа продукта для изменения не может быть пустым");

            ProductTypeProperty? modifyingProperty = await context.TypeProperties.FirstOrDefaultAsync(p => p.Id == property.Id);

            if(modifyingProperty is null)
                return Error.NotFound(description: $"Свойство типа товара для изменения с id: {property.Id} не было найдено");

            modifyingProperty.Name = property.Name;

            context.Update(modifyingProperty);
            await context.SaveChangesAsync();

            return Result.Updated;
        }

        public async Task<ErrorOr<Created>> AddPropertiesValuesForProductAsync(Product product)
        {
            Product? productInDb = await context.Products.AsNoTracking()
                                                         .Include(p => p.Type)
                                                         .Where(p => p.Id == product.Id)
                                                         .FirstOrDefaultAsync();

            if (productInDb is null)
                return Error.Validation(description: "Нельзя добавить пустые значения характеристик несуществующему продукту");


            if (productInDb.TypeId is null)
                return Error.Validation(description: "Ошибка добавления значения характеристик продукту. У продукта отсутствует тип продукта");


            List<long> propertiesId = await context.TypeProperties.AsNoTracking()
                                                                  .Where(tp => tp.TypeId == productInDb.TypeId)
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
            await context.SaveChangesAsync();

            return Result.Created;
        }
    }
}
