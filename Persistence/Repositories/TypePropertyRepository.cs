
using Application;
using Application.Common.Errors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class TypePropertyRepository(ApplicationDbContext context) : ITypePropertyRepository
    {
        public async Task<Result> AddProperty(TypeProperty property)
        {
            if (property is null)
                return new ErrorResult(message: "Свойство типа продукта не может быть пустым",
                                       errors: [ErrorList.IsNull]);

            await context.TypeProperties.AddAsync(property);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result> DeleteProperty(long id)
        {
            TypeProperty? property = await context.TypeProperties.FirstOrDefaultAsync(p => p.Id == id);

            if (property is null)
                return new NotFoundErrorResult(message: $"Свойство типа продукта с id: {id} не найдено",
                                               errors: [ErrorList.NotFound]);

            context.TypeProperties.Remove(property);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }

        public async Task<Result<List<TypeProperty>>> GetPropertiesByTypeId(long productTypeId)
        {
            ProductType? productType = await context.ProductTypes.AsNoTracking()
                                                                 .FirstOrDefaultAsync(pt => pt.Id == productTypeId);

            if (productType is null)
                return new NotFoundErrorResult<List<TypeProperty>>(message: $"Не найден тип продукта с id: {productTypeId}");

            List<TypeProperty> list = await context.TypeProperties.AsNoTracking()
                                                                  .Where(p => p.TypeId == productTypeId)
                                                                  .ToListAsync();

            return new SuccessResult<List<TypeProperty>>(list);
        }

        public async Task<Result> UpdateProperty(TypeProperty property)
        {
            if(property is null)
                return new ErrorResult(message: "Свойство типа продукта для изменения не может быть пустым",
                                       errors: [ErrorList.IsNull]);

            TypeProperty? modifyingProperty = await context.TypeProperties.FirstOrDefaultAsync(p => p.Id ==  property.Id);

            if(modifyingProperty is null)
                return new NotFoundErrorResult(message: $"Свойство типа товара для изменения с id: {property.Id} не было найдено",
                                               errors: [ErrorList.NotFound]);

            modifyingProperty.Name = property.Name;

            context.Update(property);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }
    }
}
