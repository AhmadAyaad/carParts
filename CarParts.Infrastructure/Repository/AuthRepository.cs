using CarParts.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarParts.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (!VerifyPassHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }


        public async Task<User> Register(User user, string password)
        {
            byte[] passHash, passSalt;
            CreatePasswordHash(password, out passHash, out passSalt);
            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<bool> UserExists(string email)
        {
            if (email != null)
            {
                var user = await _context.Users.AnyAsync(u => u.Email == email);
                if (user)
                    return true;
                return false;
            }
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA1())
            {
                passSalt = hmac.Key;
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA1(passwordSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }
    }
}
