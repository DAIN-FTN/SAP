using System;

namespace SAP_API.DTOs.Responses.Role
{
    public class CreateRoleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
