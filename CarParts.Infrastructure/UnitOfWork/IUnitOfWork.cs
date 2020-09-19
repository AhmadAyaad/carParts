using CarParts.Infrastructure.Repository;
using CarParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRepository<Order> GenericOrderRepository { get; }
        public IRepository<Product> GenericProductRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IUserRepository UserRepository { get; }
        Task<bool> SaveChangesAysnc();
    }
}
