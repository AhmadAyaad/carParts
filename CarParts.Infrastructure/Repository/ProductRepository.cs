using CarParts.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.Repository
{
    public class ProductRepository : IRepository<Product>, IProductRepository
    {
        readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Create(Product product)
        {
            if (product != null)
            {
                try
                {
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                try
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            if (products != null)
                return products;
            return new List<Product>();
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.Include(p => p.Photos)
                          .SingleOrDefaultAsync(p => p.ProductId == id);
            if (product != null)
                return product;
            return new Product();
        }

        public async Task Update(Product product, int id)
        {
            try
            {
                var productFromRepo = await _context.Products.FindAsync(id);
                productFromRepo.CategoryId = product.CategoryId;
                productFromRepo.OrderId = product.OrderId;
                productFromRepo.CreatedAt = product.CreatedAt;
                productFromRepo.Descirption = product.Descirption;
                productFromRepo.HasDiscount = product.HasDiscount;
                productFromRepo.PercentageOfDiscount = product.PercentageOfDiscount;
                productFromRepo.Photos = product.Photos;
                productFromRepo.Price = product.Price;
                productFromRepo.ProductName = product.ProductName;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Photo> GetProductPhoto(int photoId, int productId)
        {
            var photo = await _context.Products.SelectMany(p => p.Photos)
                    .SingleOrDefaultAsync(p => (p.Id == photoId && p.ProductId == productId));
            if (photo != null)
                return photo;
            return new Photo();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Product>> GetProductQuantityLessThanZero(IEnumerable<Product> products)
        {

            var notAvaiableProducts = products.Where(p => p.Quantity == 0).AsEnumerable();
            if (notAvaiableProducts.Count() != 0)
                return await Task.FromResult(notAvaiableProducts);

            return (IEnumerable<Product>)await Task.FromResult<Object>(null);
        }

        public List<string> GetNotAviableProductNames(ref IEnumerable<Product> products)
        {
            var productNames = products.Select(p => p.ProductName).ToList();
            return productNames;
        }
    }
}
