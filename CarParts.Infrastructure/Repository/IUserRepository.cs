using CarParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task<User> UpdateUserInfo(User user, int id);
        Task DeleteUser(int id);
        Task<User> GetUser(int id);
        Task<IEnumerable<Order>> GetUserOrders(int userId);

    }
}
