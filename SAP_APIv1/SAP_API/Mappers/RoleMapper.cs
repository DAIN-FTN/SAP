using SAP_API.DTOs.Requests.Role;
using SAP_API.DTOs.Responses.Role;
using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.Mappers
{
    public class RoleMapper
    {
        internal static Role CreateRoleRequestToRole(CreateRoleRequest body)
        {
            return new Role
            {
                Id = Guid.NewGuid(),
                Name = body.Name,
                Description = body.Description
            };
        }

        internal static CreateRoleResponse RoleToCreateRoleResponse(Role role)
        {
            return new CreateRoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }

        internal static List<RoleResponse> RoleListToRoleResponseList(List<Role> roles)
        {
            List<RoleResponse> response = new List<RoleResponse>();

            foreach(Role role in roles)
            {
                response.Add(RoleToRoleResponse(role));
            }

            return response;
        }

        private static RoleResponse RoleToRoleResponse(Role role)
        {
            return new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }
    }
}
