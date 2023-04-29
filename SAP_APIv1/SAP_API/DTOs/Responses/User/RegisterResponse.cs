using System;

namespace SAP_API.DTOs.Responses
{
    public class RegisterResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public string Role { get; set; }
    }
}
