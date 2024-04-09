using Application;
using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
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

            if(propertyValue is null)
                return new NotFoundErrorResult(message: "Значение свойства для удаления не было найдено",
                                               errors: [ErrorList.NotFound]);

            context.PropertyValues.Remove(propertyValue);
            await context.SaveChangesAsync();

            return new SuccessResult();
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

        public async Task<Result> UpdatePropertyValue(PropertyValue propertyValue)
        {
            if(propertyValue is null)
                return new ErrorResult(message: "Значение свойства для изменения не может быть пустым",
                                       errors: [ErrorList.IsNull]);

            PropertyValue? modifyingPropertyValue = await context.PropertyValues.FirstOrDefaultAsync(pv => pv.Id == propertyValue.Id);

            if(modifyingPropertyValue is null)
                return new NotFoundErrorResult(message: $"Значение свойства с id: {propertyValue.Id} не было найдено",
                                               errors: [ErrorList.NotFound]);    

            modifyingPropertyValue.Value = propertyValue.Value;

            context.Update(modifyingPropertyValue);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }
    }
}
