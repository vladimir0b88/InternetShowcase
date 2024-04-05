﻿using Application.Common.Models;
using Application.Models.Products.Create;
using Application.Models.Products.Update;
using Domain.Abstractions;

namespace Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<Result<Product>> GetProductById(long id);

        Task<Result> DeleteProductById(long id);

        Task<Result> AddProduct(ProductCreateDto productDto);

        Task<Result<List<Product>>> GetAllProducts();

        Task<Result> UpdateProduct(ProductUpdateDto updateDto);

        Task<Result<List<Product>>> GetByProductTypeId(long productTypeId);
    }
}
