using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Models;
using ShopBridge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;


        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {

            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductModel productModel)
        {
            var id = await _productRepository.AddProductAsync(productModel);
            return CreatedAtAction(nameof(GetProductById), new { id = id, Controller = "Product" }, id);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductModel ProductModel, int id)
        {
            await _productRepository.UpdateProductAsync(id, ProductModel);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProductByPatch([FromBody] JsonPatchDocument productModel, [FromRoute] int id)
        {
            await _productRepository.UpdateProductPatchAsync(id, productModel);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById([FromRoute] int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return Ok();
        }
    }
}
