using SAP_API.DTOs.Responses;
using SAP_API.Models;

namespace SAP_API.Services
{
    public interface IUserService
    {
        public User AuthenticateUser(string username, string password);
        public RegisterResponse RegisterUser(string username, string password);
    }
}
