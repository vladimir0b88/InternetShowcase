using Application.Models;
using Domain.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class TypePropertyRepository(ApplicationDbContext context) : ITypePropertyRepository
    {
        public async Task<ErrorOr<Created>> InsertAsync(TypeProperty property)
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
            TypeProperty? property = await context.TypeProperties.FirstOrDefaultAsync(p => p.Id == id);

            if (property is null)
                return Error.NotFound(description: $"Свойство типа продукта с id: {id} не найдено");

            context.TypeProperties.Remove(property);
            await context.SaveChangesAsync();

            return Result.Deleted;
        }

        public async Task<ErrorOr<List<TypeProperty>>> GetAllAsync()
        {
            List<TypeProperty> list = await context.TypeProperties.AsNoTracking()
                                                                  .ToListAsync();

            return list;
        }

        public async Task<ErrorOr<List<TypeProperty>>> GetByTypeIdAsync(long productTypeId)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking()
                                                                 .FirstOrDefaultAsync(pt => pt.Id == productTypeId);

            if (productType is null)
                return Error.NotFound(description: $"Не найден тип продукта с id: {productTypeId}");

            List<TypeProperty> list = await context.TypeProperties.AsNoTracking()
                                                                  .Where(p => p.TypeId == productTypeId)
                                                                  .ToListAsync();

            return list;
        }

        public async Task<ErrorOr<TypeProperty>> GetByIdAsync(long propertyId)
        {
            TypeProperty? typeProperty = await context.TypeProperties.AsNoTracking()
                                                                     .FirstOrDefaultAsync (tp => tp.Id == propertyId);

            if(typeProperty is null)
                return Error.NotFound(description: $"Свойство с id: {propertyId} не найден");

            return typeProperty;
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(TypeProperty property)
        {
            if(property is null)
                return Error.Validation(description: "Свойство типа продукта для изменения не может быть пустым");

            TypeProperty? modifyingProperty = await context.TypeProperties.FirstOrDefaultAsync(p => p.Id == property.Id);

            if(modifyingProperty is null)
                return Error.NotFound(description: $"Свойство типа товара для изменения с id: {property.Id} не было найдено");

            modifyingProperty.Name = property.Name;

            context.Update(modifyingProperty);
            await context.SaveChangesAsync();

            return Result.Updated;
        }

        public Task<ErrorOr<Created>> AddPropertiesValuesForProductAsync()
        {
            throw new NotImplementedException();
        }
    }
}
