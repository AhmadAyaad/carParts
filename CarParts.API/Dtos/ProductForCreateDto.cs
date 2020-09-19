using CarParts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParts.API.Dtos
{
    public class ProductForCreateDto
    {
        public string ProductName { get; set; }
        public string Descirption { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        public bool HasDiscount { get; set; } = false;
        public float? PercentageOfDiscount { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();
        public int? OrderId { get; set; }

    }
}
