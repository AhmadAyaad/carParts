using CarParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.Repository
{
    public interface IProductRepository
    {
        public Task<Photo> GetProductPhoto(int photoId, int productId);
        public Task<bool> SaveChangesAsync();
        public Task<IEnumerable<Product>> GetProductQuantityLessThanZero(IEnumerable<Product> products);

        public List<string> GetNotAviableProductNames(ref IEnumerable<Product> products);
    }

}
