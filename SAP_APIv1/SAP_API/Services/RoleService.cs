

using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs.Requests.Role;
using SAP_API.DTOs.Responses.Role;
using SAP_API.Exceptions;
using SAP_API.Mappers;
using SAP_API.Models;
using System.Collections.Generic;

namespace SAP_API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public CreateRoleResponse CreateRole(CreateRoleRequest body)
        {
            CheckIfNameUnique(body.Name);
            Role role = RoleMapper.CreateRoleRequestToRole(body);

            Role createdRole = _roleRepository.Create(role);

            return RoleMapper.RoleToCreateRoleResponse(createdRole);
        }

        private void CheckIfNameUnique(string name)
        {
            Role role = _roleRepository.GetByName(name);
            if (role != null)
                throw new UniqueConstraintViolationException("Name must be unique");
        }

        public List<RoleResponse> GetAll()
        {
            List<Role> roles = (List<Role>)_roleRepository.GetAll();
            return RoleMapper.RoleListToRoleResponseList(roles);

        }
    }
}
