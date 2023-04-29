

using System.ComponentModel.DataAnnotations;

namespace SAP_API.DTOs.Requests.Role
{
    public class CreateRoleRequest
    {
        [Required]
        public string? Name { get; set; }
        public string Description { get; set; }
    }
}
