using SAP_API.Models;

namespace SAP_API.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
