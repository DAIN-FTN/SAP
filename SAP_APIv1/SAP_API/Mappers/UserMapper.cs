using SAP_API.DTOs.Responses;
using SAP_API.Models;

namespace SAP_API.Mappers
{
    public class UserMapper
    {
        public static RegisterResponse UserToRegisterResponse(User user)
        {
            return new RegisterResponse
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };
        }
    }
}
