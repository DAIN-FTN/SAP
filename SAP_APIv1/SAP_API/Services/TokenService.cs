using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SAP_API.Models;
using System.Security.Claims;
using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using SAP_API.Options;

namespace SAP_API.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        public TokenService(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }
        public string GenerateToken(User user)
        {
            var key = _jwtOptions.Key;
            var encodedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredentials = new SigningCredentials(encodedKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("role", user.Role.Name.ToString()),
             };

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwtOptions.ExpirationTimeInHours),
                signingCredentials: signingCredentials
        );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtPayload ExtractPayloadFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                    ValidateIssuer = _jwtOptions.ValidateIssuer,
                    ValidateAudience = _jwtOptions.ValidateAudience,
                    ValidateLifetime = _jwtOptions.ValidateLifetime,
                    ValidAudience = _jwtOptions.Audience,
                    ValidIssuer = _jwtOptions.Issuer,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.Key))
            };

            // Validate and decode the token
            tokenHandler.ValidateToken(token, validationParameters, out var securityToken);

            // Get the payload from the validated security token
            var jwtPayload = ((JwtSecurityToken)securityToken).Payload;

            return jwtPayload;
        }
    }
}
