using SAP_API.DTOs.Responses.User;
using System;
using System.Collections.Generic;


namespace SAP_API.DTOs.Responses
{
    public class UserDetailsResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public string Role { get; set; }
        public bool Active { get; set; }
        public List<PreparedBakingProgramResponse> PreparedPrograms { get; set; }
    }
}
