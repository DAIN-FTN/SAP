using Microsoft.AspNetCore.Mvc;
using SAP_API.DTOs.Requests;

using SAP_API.Services;
using SAP_API.Models;
using System;
using Microsoft.AspNetCore.Http;
using SAP_API.DTOs.Responses;
using SAP_API.Exceptions;

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
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                User user = _userService.AuthenticateUser(request.UserName, request.Password);

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

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest body)
        {
            try
            {
                RegisterResponse user = _userService.RegisterUser(body.UserName, body.Password);
                return Ok();
            }
            catch(UniqueConstraintViolationException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
