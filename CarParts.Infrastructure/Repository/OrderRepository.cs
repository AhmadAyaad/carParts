using CarParts.Model;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.Repository
{
    public class OrderRepository : IRepository<Order>, IOrderRepository
    {
        readonly DataContext _context;
        readonly IUserRepository _userRepository;
        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        public OrderRepository(DataContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task Create(Order order)
        {
            if (order != null)
            {
                try
                {
                    await _context.Orders.AddAsync(order);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                try
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = await _context.Orders.ToListAsync();
            if (orders != null)
                return orders;
            return new List<Order>();
        }

        public async Task<Order> GetById(int id)
        {
            var order = await _context.Orders.Include(o => o.Products)
                      .SingleOrDefaultAsync(o => o.OrderId == id);
            if (order != null)
                return order;
            return new Order();
        }

        public Task<IEnumerable<Order>> GetUserOrders(int userId)
        {
            var orders = _userRepository.GetUserOrders(userId);
            return orders;
        }

        public async Task Update(Order order, int id)
        {
            try
            {
                var updatedOrder = await _context.Orders.FindAsync(id);
                updatedOrder.CreatedAt = order.CreatedAt;
                updatedOrder.DeliveredAt = order.DeliveredAt;
                updatedOrder.Item = order.Item;
                updatedOrder.OrderStatus = order.OrderStatus;
                updatedOrder.Products = order.Products;

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
