using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Api.Token
{
    public class GeneratorTokenService 
    {
        private readonly IConfiguration _configuration;

        public GeneratorTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"])); 

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = ManageClains(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(3),

            };

            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }

        private ClaimsIdentity ManageClains(User user)
        {
            var cli = new ClaimsIdentity();
            cli.AddClaim(new Claim(ClaimTypes.Name, user.Email));

            foreach (var role in user.Roles)
                cli.AddClaim(new Claim(ClaimTypes.Role, role));

            return cli; 
        }
    }
}