using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Exceptions;
using SAP_API.Services;
using System;

namespace SAP_API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest body, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                RegisterResponse response = _userService.RegisterUser(body);
                return Ok(response);
            }
            catch (UniqueConstraintViolationException ex)
            {
                ModelState.AddModelError("ErrorToDisplay", ex.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
