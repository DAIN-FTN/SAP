using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.User;
using SAP_API.Models;
using System;
using System.Collections.Generic;

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
                Password = user.Password,
                RoleId = user.RoleId,
                Role = user.Role.Name
            };
        }

        internal static User RegisterRequestToUser(RegisterRequest body)
        {
            return new User
            {
                Id = new Guid(),
                Username = body.Username,
                Password = body.Password,
                RoleId = (Guid)body.RoleId
            };
        }

        internal static List<UserResponse> UserListToUserResponseList(List<User> users)
        {
            List<UserResponse> response = new List<UserResponse>();
           foreach (User user in users)
            {
                response.Add(UserToUserResponse(user));
            }
            return response;
        }

        private static UserResponse UserToUserResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Role = user.Role.Name,
                RoleId = user.RoleId,
                Active = user.Active
            };
        }

        internal static UserDetailsResponse UserToUserDetailsResponse(User user)
        {
            return new UserDetailsResponse
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Role = user.Role.Name,
                RoleId = user.RoleId,
                Active = user.Active,
                PreparedPrograms = BakingProgramListToPreparedProgramResponseList(user.BakingProgramsMade)
            };
        }

        private static List<PreparedBakingProgramResponse> BakingProgramListToPreparedProgramResponseList(List<BakingProgram> bakingProgramsMade)
        {
            List<PreparedBakingProgramResponse> response = new List<PreparedBakingProgramResponse>();
            foreach(BakingProgram bp in bakingProgramsMade)
            {
                response.Add(BakingProgramToPreparedBakingProgramResponse(bp));
            }

            return response;
        }

        private static PreparedBakingProgramResponse BakingProgramToPreparedBakingProgramResponse(BakingProgram program)
        {
            return new PreparedBakingProgramResponse
            {
                Id = program.Id,
                Code = program.Code,
                CreatedAt = program.CreatedAt,
                Status = program.Status,
                BakingTimeInMins = program.BakingTimeInMins,
                BakingTempInC = program.BakingTempInC,
                BakingProgrammedAt = program.BakingProgrammedAt,
                BakingStartedAt = program.BakingStartedAt,
                OvenCode = program.Oven.Code,
                OvenId = program.Oven.Id,
            };
        }

        internal static UpdateUserResponse UserToUpdateUserResponse(User user)
        {
            return new UpdateUserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Role = user.Role.Name,
                RoleId = user.RoleId,
                Active = user.Active
            };
        }
    }
}
