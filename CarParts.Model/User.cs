using System;
using System.Collections.Generic;

namespace CarParts.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public int Phone { get; set; }
        public UserRole UserRole { get; set; } = UserRole.NormalUser;
        public DateTime CreatedAt { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
