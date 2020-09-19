using System;
using System.Collections.Generic;
using System.Text;

namespace CarParts.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}

