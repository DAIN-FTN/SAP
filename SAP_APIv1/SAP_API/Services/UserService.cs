using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Exceptions;
using SAP_API.Mappers;
using SAP_API.Models;
using SAP_API.Utils;
using System;

namespace SAP_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHasher _hasher;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IHasher hasher, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _roleRepository = roleRepository;
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

            CheckIfRoleValid((Guid)body.RoleId);

            User userToCreate = UserMapper.RegisterRequestToUser(body);
            userToCreate.Password = _hasher.Hash(userToCreate.Password);

            User user = _userRepository.Create(userToCreate);
            return UserMapper.UserToRegisterResponse(user);
        }

        private void CheckIfRoleValid(Guid roleId)
        {
            Role role = _roleRepository.GetById(roleId);
            if (role == null)
                throw new ForeignKeyViolationException("RoleId not valid");

        }

        private void CheckIfUsernameUnique(string username)
        {
            User userWithUsername = _userRepository.GetByUsername(username);
            if (userWithUsername != null)
                throw new UniqueConstraintViolationException("Username is already taken");
        }
    }
}
