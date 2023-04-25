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
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public IdentityController(
            ITokenService tokenService, 
            IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(
            [FromBody] LoginRequest request
            )
        {
            User user = _userService.AuthenticateUser(request.UserName, request.Password);

            if(user == null)
            {
                return Unauthorized();
            }

            string token = _tokenService.GenerateToken(user);

            return Ok(token);
        }
    }
}
