using CarParts.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(u => u.Orders).SingleOrDefaultAsync(u => u.UserId == id);
            if (user != null)
                return user;
            return new User();

        }
        public async Task<IEnumerable<Order>> GetUserOrders(int userId)
        {
            var user = await GetUser(userId);
            return user.Orders;
        }

        public async Task<User> UpdateUserInfo(User newUser, int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null && newUser != null)
            {
                user.Address = newUser.Address;
                user.Cart = newUser.Cart;
                user.CreatedAt = newUser.CreatedAt;
                user.Email = newUser.Email;
                user.Name = newUser.Name;
                user.Phone = newUser.Phone;
                user.UserRole = newUser.UserRole;
                user.Orders = newUser.Orders;
                await _context.SaveChangesAsync();
                return user;
            }

            return await Task.FromResult(new User());

        }
    }
}
