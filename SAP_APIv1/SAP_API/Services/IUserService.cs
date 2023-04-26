using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.Services
{
    public interface IUserService
    {
        public User AuthenticateUser(string username, string password);
        public RegisterResponse RegisterUser(RegisterRequest body);
        List<UserResponse> GetAll(string name);
        UserDetailsResponse GetById(Guid userId);
    }
}
