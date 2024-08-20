using Application.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class PropertyValueRepository(ApplicationDbContext context) : IPropertyValueRepository
    {
        public async Task<Result> AddPropertyValue(PropertyValue propertyValue)
        {
            if (propertyValue is null)
                return new ErrorResult(message: "Значение свойства не может быть пустым",
                                       errors: [ErrorList.IsNull]);

            await context.PropertyValues.AddAsync(propertyValue);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result> DeletePropertyValueById(long propertyValueId)
        {
            PropertyValue? propertyValue = await context.PropertyValues.FirstOrDefaultAsync(pv => pv.Id == propertyValueId);

            if (propertyValue is null)
                return new NotFoundErrorResult(message: "Значение свойства для удаления не было найдено",
                                               errors: [ErrorList.NotFound]);

            context.PropertyValues.Remove(propertyValue);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result<List<PropertyValue>>> GetAllPropertyValues()
        {
            List<PropertyValue> list = await context.PropertyValues.AsNoTracking()
                                                                   .ToListAsync();

            return new SuccessResult<List<PropertyValue>>(list);
        }

        public async Task<Result<List<PropertyValue>>> GetPropertyValuesByProductId(long productId)
        {
            Product? product = await context.Products.AsNoTracking()
                                                     .FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null)
                return new NotFoundErrorResult<List<PropertyValue>>(message: $"Продукт с id: {productId} не был найден",
                                                                    errors: [ErrorList.NotFound]);

            List<PropertyValue> properties = await context.PropertyValues.AsNoTracking()
                                                                         .Where(pv => pv.ProductId == productId)
                                                                         .Include(pv => pv.TypeProperty)
                                                                         .ToListAsync();

            return new SuccessResult<List<PropertyValue>>(properties);
        }

        public async Task<Result<List<UniquePropertyValues>>> GetUniquePropertyValues(long productTypeId)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking()
                                                                 .Where(pt => pt.Id == productTypeId)
                                                                 .Include(pt => pt.Properties)
                                                                 .FirstOrDefaultAsync();

            if (productType is null)
                return new NotFoundErrorResult<List<UniquePropertyValues>>(message: $"Тип товара с id: {productTypeId} не найден",
                                                                           errors: [ErrorList.NotFound]);



            List<UniquePropertyValues> list = await (from tp in context.TypeProperties
                                                     where tp.TypeId == productTypeId
                                                     select new UniquePropertyValues()
                                                     {
                                                         TypeProperty = tp,
                                                         Values = (from pv in context.PropertyValues
                                                                   where pv.PropertyId == tp.Id && pv.Value != string.Empty
                                                                   select pv.Value).ToHashSet().ToList(),
                                                     }).ToListAsync();


            return new SuccessResult<List<UniquePropertyValues>>(list.Where(upv => upv.Values.Count > 1).ToList());
        }

        public async Task<Result> UpdatePropertyValue(PropertyValue propertyValue)
        {
            if (propertyValue is null)
                return new ErrorResult(message: "Значение свойства для изменения не может быть пустым",
                                       errors: [ErrorList.IsNull]);

            PropertyValue? modifyingPropertyValue = await context.PropertyValues.FirstOrDefaultAsync(pv => pv.Id == propertyValue.Id);

            if (modifyingPropertyValue is null)
                return new NotFoundErrorResult(message: $"Значение свойства с id: {propertyValue.Id} не было найдено",
                                               errors: [ErrorList.NotFound]);

            modifyingPropertyValue.Value = propertyValue.Value;

            context.Update(modifyingPropertyValue);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }
    }
}
