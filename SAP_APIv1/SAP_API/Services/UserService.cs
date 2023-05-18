using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Requests.User;
using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.User;
using SAP_API.Exceptions;
using SAP_API.Mappers;
using SAP_API.Models;
using SAP_API.Utils;
using System;
using System.Collections.Generic;

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

        public List<UserResponse> GetAll(string name, bool? active)
        {
            List<User> users;

            if (String.IsNullOrEmpty(name))
            {
                users = (List<User>)_userRepository.GetAll();
            }
            else
            {
                users = (_userRepository.GetUsersByUsername(name));

            }

            if (active != null)
                users = users.FindAll(u => u.Active == active);

            return UserMapper.UserListToUserResponseList(users);
        }

        public UserDetailsResponse GetById(Guid userId)
        {
            User user = _userRepository.GetById(userId);
            if (user == null)
                return null;
            return UserMapper.UserToUserDetailsResponse(user);
        }

        public RegisterResponse RegisterUser(RegisterRequest body)
        {
            CheckIfUsernameUnique(body.Username);

            CheckIfRoleValid((Guid)body.RoleId);

            User userToCreate = UserMapper.RegisterRequestToUser(body);
            userToCreate.Password = _hasher.Hash(userToCreate.Password);
            userToCreate.Active = true;

            User user = _userRepository.Create(userToCreate);
            return UserMapper.UserToRegisterResponse(user);
        }

        public UpdateUserResponse UpdateUser(UpdateUserRequest body, Guid userId)
        {
            User user = _userRepository.GetById(userId);
            if (user == null)
                return null;

            if (!user.Username.Equals(body.Username))
                CheckIfUsernameUnique(body.Username);
            CheckIfRoleValid((Guid)body.RoleId);
            if (user.Active && (bool)!body.Active)
                CheckIfUserCanBeDeactivated(user);

            UpdateUserFields(body, user);
            User updatedUser = _userRepository.Update(user);
            return UserMapper.UserToUpdateUserResponse(updatedUser);

        }

        private void CheckIfUserCanBeDeactivated(User user)
        {
            List<BakingProgram> bakingProgramsCurrentlyPreparedByUser = user.BakingProgramsMade.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Preparing));
            if (bakingProgramsCurrentlyPreparedByUser.Count > 0)
                throw new UnableToDeactivateUserException("Unable to deactivate user. There are programs user is preparing.");
        }

        private void UpdateUserFields(UpdateUserRequest body, User user)
        {
            user.RoleId = (Guid)body.RoleId;
            user.Username = body.Username;
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
