using Microsoft.AspNetCore.Mvc;
using SAP_API.DTOs.Requests;

using SAP_API.Services;
using SAP_API.Models;
using System;
using Microsoft.AspNetCore.Http;
using SAP_API.DTOs.Responses;
using SAP_API.Exceptions;
using Microsoft.Extensions.Options;

namespace SAP_API.Controllers
{
    [ApiController]
    [Route("api/auth")]
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

    
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                User user = _userService.AuthenticateUser(request.Username, request.Password);

                if (user == null)
                {
                    return Unauthorized();
                }

                string token = _tokenService.GenerateToken(user);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("Me")]
        public IActionResult Me() {
            //TODO: in case we need this logic elsewhere, implement middleware
            var authorizationHeader = Request.Headers["Authorization"];
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            try {
                 JwtPayload payload = this.tokenService.ExtractPayloadFromToken(token);
                 var user = _userService.GetById(payload.sub);
                 return Ok(user);
            } catch (Exception ex ) {
                return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
           
        }

    }
}
