using Microsoft.AspNetCore.Mvc;
using SAP_API.DTOs.Requests;

using SAP_API.Services;
using SAP_API.Models;

namespace SAP_API.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class IdentityController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly IUserService userService;

        public IdentityController(
            ITokenService tokenService, 
            IUserService userService)
        {
            this.tokenService = tokenService;
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult Login(
            [FromBody] LoginRequest request
            )
        {
            User user = this.userService.AuthenticateUser(request.UserName, request.Password);

            if(user == null)
            {
                return Unauthorized();
            }

            string token = this.tokenService.GenerateToken(user);

            return Ok(token);
        }
    }
}
