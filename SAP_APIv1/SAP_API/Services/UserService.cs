using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Exceptions;
using SAP_API.Mappers;
using SAP_API.Models;
using SAP_API.Utils;

namespace SAP_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;

        public UserService(IUserRepository userRepository, IHasher hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }
        public User AuthenticateUser(string username, string password)
        {
            User user = _userRepository.GetByUsername(username);
            if (user == null)
                return null;

            //TODO needsUpgrade
            (bool verified, bool needsUpgrade) = _hasher.Check(user.Password, password);

            return verified ? user : null;
        }




        public RegisterResponse RegisterUser(RegisterRequest body)
        {
            CheckIfUsernameUnique(body.Username);

            User userToCreate = UserMapper.RegisterRequestToUser(body);
            userToCreate.Password = _hasher.Hash(userToCreate.Password);

            User user = _userRepository.Create(userToCreate);
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
