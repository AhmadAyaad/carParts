using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarParts.Infrastructure.Repository;
using CarParts.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarParts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        readonly IRepository<Category> _categoryRepository;

        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAll();
            if (categories != null)
                return Ok(categories);
            return NotFound();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category != null)
                return Ok(category);
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            try
            {
                if (category != null)
                {
                    await _categoryRepository.Create(category);
                    return StatusCode(201);
                }
                return BadRequest("category cannot be empty");
            }
            catch
            {
                return BadRequest("can not create the category");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            try
            {
                await _categoryRepository.Update(category, id);
                return Ok();
            }
            catch
            {
                return BadRequest("cannot update the category");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _categoryRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest("cannot delete the category");
            }
        }
    }
}
