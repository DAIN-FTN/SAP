using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Models;
using System;

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

        internal static User RegisterRequestToUser(RegisterRequest body)
        {
            return new User
            {
                Id = new Guid(),
                Username = body.Username,
                Password = body.Password
            };
        }
    }
}
