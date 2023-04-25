using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs.Responses;
using SAP_API.Exceptions;
using SAP_API.Mappers;
using SAP_API.Models;

namespace SAP_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User AuthenticateUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public RegisterResponse RegisterUser(string username, string password)
        {
            CheckIfUsernameUnique(username);

            User user = _userRepository.Create(new User { Username = username, Password = password });
            return UserMapper.UserToRegisterResponse(user);
        }

        private void CheckIfUsernameUnique(string username)
        {
            User userWithUsername = _userRepository.GetByUsername(username);
            if (userWithUsername != null)
                throw new UniqueConstraintViolationException("Username is already taken");
        }
    }
}
