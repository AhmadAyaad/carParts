using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarParts.API.Dtos;
using CarParts.Infrastructure.Repository;
using CarParts.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarParts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        readonly IAuthRepository _authRepository;
        readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForCreateDto userForCreateDto)
        {
            userForCreateDto.Name = userForCreateDto.Name.ToLower();
            if (await _authRepository.UserExists(userForCreateDto.Email))
                return BadRequest("User already exists");
            var user = new User
            {
                Email = userForCreateDto.Email,
                Name = userForCreateDto.Name,
                Phone = userForCreateDto.Phone,
                CreatedAt = userForCreateDto.CreatedAt,
                Address = userForCreateDto.Address,
                UserRole = userForCreateDto.UserRole
            };
            await _authRepository.Register(user, userForCreateDto.Password);
            return StatusCode(201);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _authRepository.Login(userForLoginDto.Email, userForLoginDto.Password);
            if (user == null)
                return Unauthorized();

            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier ,  user.UserId.ToString()) ,
                    new Claim(ClaimTypes.Email , userForLoginDto.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = creds,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userToReturn = new UserForListDto
            {
                Name = user.Name,
                Phone = user.Phone,
                Orders = user.Orders,
                CreatedAt = user.CreatedAt,
                Address = user.Address,
                UserRole = user.UserRole
            };

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                userToReturn
            });

        }
    }
}
