using CarParts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParts.API.Dtos
{
    public class OrderForListDto
    {
        public int OrderId { get; set; }
        public string Item { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DeliveredAt { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;
        public int? UserId { get; set; }
        public int? CartId { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
