using Application.Common;
using Application.Models;
using Domain.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class PropertyValueRepository(ApplicationDbContext context) : IPropertyValueRepository
    {
        public async Task<ErrorOr<Created>> InsertAsync(PropertyValue propertyValue)
        {
            if (propertyValue is null)
                return Error.Validation("Значение свойства не может быть пустым");

            await context.PropertyValues.AddAsync(propertyValue);
            await context.SaveChangesAsync();

            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> DeleteByIdAsync(long propertyValueId)
        {
            PropertyValue? propertyValue = await context.PropertyValues.FirstOrDefaultAsync(pv => pv.Id == propertyValueId);

            if (propertyValue is null)
                return Error.NotFound(description: "Значение свойства для удаления не было найдено");

            context.PropertyValues.Remove(propertyValue);
            await context.SaveChangesAsync();

            return Result.Deleted;
        }

        public async Task<ErrorOr<List<PropertyValue>>> GetAllAsync()
        {
            List<PropertyValue> list = await context.PropertyValues.AsNoTracking()
                                                                   .ToListAsync();

            return list;
        }

        public async Task<ErrorOr<List<PropertyValue>>> GetByProductIdAsync(long productId)
        {
            Product? product = await context.Products.AsNoTracking()
                                                     .FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null)
                return Error.NotFound(description: $"Продукт с id: {productId} не был найден");

            List<PropertyValue> properties = await context.PropertyValues.AsNoTracking()
                                                                         .Where(pv => pv.ProductId == productId)
                                                                         .Include(pv => pv.TypeProperty)
                                                                         .ToListAsync();

            return properties;
        }

        public async Task<ErrorOr<List<UniquePropertyValues>>> GetUniquesByProductTypeIdAsync(long productTypeId)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking()
                                                                 .Where(pt => pt.Id == productTypeId)
                                                                 .Include(pt => pt.Properties)
                                                                 .FirstOrDefaultAsync();

            if (productType is null)
                return Error.NotFound(description: $"Тип товара с id: {productTypeId} не найден");


            List<UniquePropertyValues> list = await (from tp in context.TypeProperties
                                                     where tp.TypeId == productTypeId
                                                     select new UniquePropertyValues()
                                                     {
                                                         TypeProperty = tp,
                                                         Values = (from pv in context.PropertyValues
                                                                   where pv.PropertyId == tp.Id && pv.Value != string.Empty
                                                                   select pv.Value).ToHashSet().ToList(),
                                                     }).ToListAsync();


            return list.Where(upv => upv.Values.Count > 1)
                       .ToList();
        }

        public async Task<ErrorOr<Updated>> UpdateAsync(PropertyValue propertyValue)
        {
            if (propertyValue is null)
                return Error.Validation("Значение свойства для изменения не может быть пустым");

            PropertyValue? modifyingPropertyValue = await context.PropertyValues.FirstOrDefaultAsync(pv => pv.Id == propertyValue.Id);

            if (modifyingPropertyValue is null)
                return Error.Validation($"Значение свойства с id: {propertyValue.Id} не было найдено");

            modifyingPropertyValue.Value = propertyValue.Value;

            context.Update(modifyingPropertyValue);
            await context.SaveChangesAsync();

            return Result.Updated;
        }
    }
}
