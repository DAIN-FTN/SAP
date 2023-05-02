using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Requests.User;
using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.User;
using SAP_API.Exceptions;
using SAP_API.Services;
using System;
using System.Collections.Generic;

namespace SAP_API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
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
            catch(ForeignKeyViolationException ex)
            {
                ModelState.AddModelError("ErrorToDisplay", ex.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetAll([FromQuery] string name, [FromQuery] bool? active)
        {
            try
            {
                List<UserResponse> response = _userService.GetAll(name, active);

                if (response == null || response.Count == 0)
                    return NoContent();

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpGet("{userId}")]
        [Authorize]
        public IActionResult GetById(Guid userId)
        {
            try
            {
                UserDetailsResponse response = _userService.GetById(userId);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpPut("{userId}")]
        [Authorize]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest body, Guid userId, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                UpdateUserResponse response = _userService.UpdateUser(body, userId);

                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (UniqueConstraintViolationException ex)
            {
                ModelState.AddModelError("ErrorToDisplay", ex.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (UnableToDeactivateUserException ex)
            {
                ModelState.AddModelError("ErrorToDisplay", ex.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (ForeignKeyViolationException ex)
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
