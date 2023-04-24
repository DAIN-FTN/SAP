using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SAP_API.Models;
using System.Data;
using System.Net;
using System.Security.Claims;
using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace SAP_API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            var key = configuration["Jwt:Key"];
            var encodedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(encodedKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
             };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(int.Parse((configuration["Jwt:ExpirationTimeInHours"]))),
                signingCredentials: signingCredentials
        );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
