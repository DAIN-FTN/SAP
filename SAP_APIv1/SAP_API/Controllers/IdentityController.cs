using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAP_API.DTOs.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Security.Claims;
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
