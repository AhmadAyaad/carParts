using CarParts.Model;
using System;
using System.Collections.Generic;

namespace CarParts.API.Controllers.Dtos
{
    public class OrderForCreateDto
    {
        public string Item { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Created;
        public int UserId { get; set; }
        public int CartId { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}