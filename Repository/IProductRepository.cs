using Microsoft.AspNetCore.JsonPatch;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProducts();
        Task<ProductModel> GetProductByIdAsync(int ProductId);
        Task<int> AddProductAsync(ProductModel product);
        Task UpdateProductAsync(int ProductId, ProductModel ProductModel);
        Task UpdateProductPatchAsync(int ProductId, JsonPatchDocument productModel);
        Task DeleteProductAsync(int id);
    }
}
