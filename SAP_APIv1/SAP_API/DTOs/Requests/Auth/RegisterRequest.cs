
using System.ComponentModel.DataAnnotations;

namespace SAP_API.DTOs.Requests
{
    public class RegisterRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password {get; set;}
}
}
