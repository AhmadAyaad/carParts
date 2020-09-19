using CarParts.Infrastructure.Repository;
using CarParts.Model;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        DataContext _context;
        IRepository<Order> _genericOrderRepository;
        IOrderRepository _orderRepository;
        IRepository<Product> _genericproductRepository;
        IProductRepository _productRepository;
        IUserRepository _userRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IRepository<Order> GenericOrderRepository
        {
            get
            {
                if (_genericOrderRepository == null)
                    return _genericOrderRepository = new OrderRepository(_context);
                return _genericOrderRepository;
            }
        }
        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                    return _orderRepository = new OrderRepository(_context);
                return _orderRepository;
            }
        }
        public IRepository<Product> GenericProductRepository
        {
            get
            {
                if (_genericproductRepository == null)
                    return _genericproductRepository = new ProductRepository(_context);
                return _genericproductRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    return _productRepository = new ProductRepository(_context);
                return _productRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    return _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        public async Task<bool> SaveChangesAysnc()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
