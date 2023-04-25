using System.ComponentModel.DataAnnotations;

namespace SAP_API.DTOs.Requests
{
    public class LoginRequest
    {
        [Required]
        public readonly string? UserName;
        [Required]
        public readonly string? Password;
    }

}