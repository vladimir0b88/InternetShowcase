﻿using Application.Common;
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

        public async Task<Result<List<TypeProperty>>> GetAllTypeProperties()
        {
            List<TypeProperty> list = await context.TypeProperties.AsNoTracking()
                                                                  .ToListAsync();

            return new SuccessResult<List<TypeProperty>>(list);
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

        public async Task<Result<TypeProperty>> GetPropertyById(long propertyId)
        {
            TypeProperty? typeProperty = await context.TypeProperties.AsNoTracking()
                                                                     .FirstOrDefaultAsync (tp => tp.Id == propertyId);

            if(typeProperty is null)
                return new NotFoundErrorResult<TypeProperty>(message: $"Свойство с id: {propertyId} не найден",
                                                             errors: [ErrorList.NotFound]);

            return new SuccessResult<TypeProperty>(typeProperty);
        }

        public async Task<Result> UpdateProperty(TypeProperty property)
        {
            if(property is null)
                return new ErrorResult(message: "Свойство типа продукта для изменения не может быть пустым",
                                       errors: [ErrorList.IsNull]);

            TypeProperty? modifyingProperty = await context.TypeProperties.FirstOrDefaultAsync(p => p.Id == property.Id);

            if(modifyingProperty is null)
                return new NotFoundErrorResult(message: $"Свойство типа товара для изменения с id: {property.Id} не было найдено",
                                               errors: [ErrorList.NotFound]);

            modifyingProperty.Name = property.Name;

            context.Update(modifyingProperty);
            await context.SaveChangesAsync();

            return new SuccessResult();
        }
    }
}
