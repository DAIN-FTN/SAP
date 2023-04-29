

using SAP_API.DTOs.Requests.Role;
using SAP_API.DTOs.Responses.Role;
using System.Collections.Generic;

namespace SAP_API.Services
{
    public interface IRoleService
    {
        public List<RoleResponse> GetAll();
        public CreateRoleResponse CreateRole(CreateRoleRequest body);
    }
}
