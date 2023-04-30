using SAP_API.Models;
using System.IdentityModel.Tokens.Jwt;

namespace SAP_API.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        JwtPayload ExtractPayloadFromToken(string token);
    }
}
