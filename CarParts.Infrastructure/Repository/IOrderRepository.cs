using CarParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetUserOrders(int userId);
    }
}
