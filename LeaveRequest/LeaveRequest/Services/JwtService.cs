using LeaveRequest.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LeaveRequest.Services
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly string _expDate;
        private readonly MyContext myContext;

        public JwtService(IConfiguration _config)
        {
            _secret = _config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = _config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }

        public string GenerateSecurityToken(string name, string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, name),
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, role),
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            tokenDescriptor.Audience = "localhost:44338";
            tokenDescriptor.Issuer = "localhost:44338";
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
