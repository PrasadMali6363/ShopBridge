using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ShopBridge.ProductData;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }
        public async Task<List<ProductModel>> GetAllProducts()
        {
            var records = await _context.Products.Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price

            }).ToListAsync();

            return records;

        }

        public async Task<ProductModel> GetProductByIdAsync(int ProductId)
        {
            var records = await _context.Products.Where(x => x.Id == ProductId).Select(x => new ProductModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price

            }).FirstOrDefaultAsync();

            return records;

        }

        public async Task<int> AddProductAsync(ProductModel product)
        {
            var productdata = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };

            _context.Products.Add(productdata);
            await _context.SaveChangesAsync();
            return productdata.Id;
        }

        public async Task UpdateProductAsync(int ProductId, ProductModel ProductModel)
        {
            var Product = await _context.Products.FindAsync(ProductId);
            if (Product != null)
            {
                Product.Name = ProductModel.Name;
                Product.Description = ProductModel.Description;
                Product.Price = ProductModel.Price;

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProductPatchAsync(int ProductId, JsonPatchDocument productModel)
        {
            var Product = await _context.Products.FindAsync(ProductId);
            if (Product != null)
            {
                productModel.ApplyTo(Product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var ProductData = await _context.Products.FindAsync(id);
            if (ProductData != null)
            {
                _context.Products.Remove(ProductData);
                await _context.SaveChangesAsync();
            }

        }

    }
}
