using System;
using System.Collections.Generic;
using System.Text;

namespace CarParts.Model
{
    public class Cart
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
