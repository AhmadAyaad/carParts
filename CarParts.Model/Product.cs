using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CarParts.Model
{
    public class Product
    {
        /// <summary>
        /// need to see what will be handled through concurrecny
        /// </summary>
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Descirption { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
        public bool HasDiscount { get; set; } = false;
        public float? PercentageOfDiscount { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        /// <summary>
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        /// </summary>

        public ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();
    }
}
