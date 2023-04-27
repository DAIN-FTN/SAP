

using System;
using System.ComponentModel.DataAnnotations;

namespace SAP_API.DTOs.Requests.User
{
    public class UpdateUserRequest
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public Guid? RoleId { get; set; }
        [Required]
        public bool? Active { get; set; }
    }
}
