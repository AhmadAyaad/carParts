using CarParts.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Category category)
        {
            try
            {
                if (category != null)
                {
                    await _context.Categories.AddAsync(category);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                try
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            if (categories != null)
                return categories;
            return new List<Category>();
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category != null ? category : new Category();
        }

        public async Task Update(Category category, int id)
        {
            try
            {
                var newCategory = await _context.Categories.FindAsync(id);
                newCategory.Name = category.Name;
                newCategory.Products = category.Products;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
