using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MenuItemListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        public static List<MenuItem> menulist = new List<MenuItem>()
        {
            new MenuItem { Id = 1, Name = "Burger", freeDelivery = true, Price = 60.00,  dateOfLaunch = DateTime.Now, Active = true },
            new MenuItem { Id = 2, Name = "Pizza", freeDelivery = true, Price = 250.00,  dateOfLaunch = DateTime.Now ,Active = true },
            new MenuItem { Id = 3, Name = "Pasta", freeDelivery = true, Price = 160.00,  dateOfLaunch = DateTime.Now  , Active = true },
            new MenuItem { Id = 4, Name = "Garlic Bread", freeDelivery = true, Price = 99.00, dateOfLaunch = DateTime.Now  ,   Active = true },
        };

        public TokenController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("TokenGenerate")]
        public IActionResult Login(User login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticteUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { token = tokenString });
            }
            return response;
        }
        User AuthenticteUser(User login)
        {
            User user = null;

    
            if (login.Name == "Shivangi")
            {
                user = new User { Name = "Shivangi Singh", Password = "1234" };
            }
            return user;
        }
        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
