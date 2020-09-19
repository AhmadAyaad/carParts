using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using CarParts.API.Dtos;
using CarParts.Infrastructure.Repository;
using CarParts.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CarParts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        readonly IRepository<Product> _productRepo;

        public ProductsController(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductForCreateDto productForCreateDto)
        {
            try
            {
                var product = new Product
                {
                    CreatedAt = productForCreateDto.CreatedAt,
                    Descirption = productForCreateDto.Descirption,
                    HasDiscount = productForCreateDto.HasDiscount,
                    PercentageOfDiscount = productForCreateDto.PercentageOfDiscount,
                    Photos = productForCreateDto.Photos,
                    ProductName = productForCreateDto.ProductName,
                    Price = productForCreateDto.Price,
                    CategoryId = productForCreateDto.CategoryId
                };

                await _productRepo.Create(product);

                return StatusCode(201);
            }
            catch (Exception)
            {
                return BadRequest("can not create the product");
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, ProductForCreateDto productForCreateDto)
        {
            try
            {
                var product = new Product
                {
                    CategoryId = productForCreateDto.CategoryId,
                    OrderId = productForCreateDto.OrderId,
                    CreatedAt = productForCreateDto.CreatedAt,
                    HasDiscount = productForCreateDto.HasDiscount,
                    PercentageOfDiscount = productForCreateDto.PercentageOfDiscount,
                    Photos = productForCreateDto.Photos,
                    Price = productForCreateDto.Price,
                    ProductName = productForCreateDto.ProductName,
                    Descirption = productForCreateDto.Descirption,
                };
                await _productRepo.Update(product, productId);
                return Ok(product);
            }
            catch
            {
                return BadRequest("can not update the product");
            }
        }
        [HttpGet("{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct(int productId)
        {
            var product = await _productRepo.GetById(productId);
            if (product != null)
                return Ok(product);
            return NotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepo.GetAll();
            if (products != null)
                return Ok(products);
            return NotFound();
        }

        [HttpDelete("{produtdId}")]
        public async Task<IActionResult> DeleteProduct(int produtdId)
        {
            await _productRepo.Delete(produtdId);
            return Ok();
        }
    }
}
