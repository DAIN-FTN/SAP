﻿using SAP_API.DataAccess.Repositories;
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

        public List<UserResponse> GetAll(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                List<User> users = (List<User>)_userRepository.GetAll();
                return UserMapper.UserListToUserResponseList(users);
            }

            return UserMapper.UserListToUserResponseList(_userRepository.GetUsersByUsername(name));
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

            UpdateUserFields(body, user);
            User updatedUser = _userRepository.Update(user);
            return UserMapper.UserToUpdateUserResponse(updatedUser);

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