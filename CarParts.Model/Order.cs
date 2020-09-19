using System;
using System.Collections.Generic;
using System.Text;

namespace CarParts.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Item { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DeliveredAt { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? CartId { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
