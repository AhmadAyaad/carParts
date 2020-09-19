using CarParts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParts.API.Dtos
{
    public class UserForListDto
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public int Phone { get; set; }
        public UserRole UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

    }
}
