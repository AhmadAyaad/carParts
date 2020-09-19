using CarParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.Repository
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);

        Task<bool> UserExists(string userName);
    }
}
