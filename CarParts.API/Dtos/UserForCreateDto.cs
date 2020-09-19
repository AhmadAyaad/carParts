using CarParts.Model;
using System;

namespace CarParts.API.Dtos
{
    public class UserForCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Phone { get; set; }
        public UserRole UserRole { get; set; } = UserRole.NormalUser;
    }
}