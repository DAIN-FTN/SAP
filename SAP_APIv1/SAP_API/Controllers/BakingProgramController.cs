using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SAP_API.DTOs;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Exceptions;
using SAP_API.Models;
using SAP_API.Services;
using SAP_API.Validation;
using System;
using System.Collections.Generic;
namespace SAP_API.Controllers
{
    [ApiController]
    [Route("api/baking-programs")]
    public class BakingProgramController : ControllerBase
    {
        private readonly IBakingProgramService _bakingProgramService;

        public BakingProgramController(IBakingProgramService bakingProgramService)
        {
            _bakingProgramService = bakingProgramService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                AllBakingProgramsResponse response = _bakingProgramService.GetBakingProgramsForUser();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpPost("available")]
        public IActionResult FindAvailableBakingPrograms([FromBody] FindAvailableBakingProgramsRequest body, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                CustomValidationResult validationResult = body.IsValid();
                if (!validationResult.Success)
                {
                    ModelState.AddModelError("ErrorToDisplay", validationResult.ErrorMessage);
                    return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
                }

                AvailableProgramsResponse response = _bakingProgramService.FindAvailableBakingPrograms(body);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpGet("start-preparing/{bakingProgramId}")]
        public IActionResult StartPreparing(Guid bakingProgramId, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                BakingProgram bakingProgram = _bakingProgramService.GetById(bakingProgramId);
                if (bakingProgram == null)
                    return NotFound();

                bool userIsAlreadyPreparingAnotherProgram = _bakingProgramService.CheckIfUserIsAlreadyPreparingAnotherProgram();
                if (userIsAlreadyPreparingAnotherProgram)
                {
                    ModelState.AddModelError("ErrorToDisplay", "There are programs that should be finished preparing.");
                    return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
                }

                bool isNextForPreparing = _bakingProgramService.CheckIfProgramIsNextForPreparing(bakingProgram);
                if (!isNextForPreparing)
                {
                    ModelState.AddModelError("ErrorToDisplay", "The program is not next for preparing.");
                    return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
                }

                StartPreparingResponse response = _bakingProgramService.GetDataForPreparing(bakingProgram);
                return Ok(response);

            }
            catch (BadProgramStatusException statusEx)
            {
                ModelState.AddModelError("ErrorToDisplay", statusEx.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
          
        }

        [HttpPut("cancell-preparing/{bakingProgramId}")]
        public IActionResult CancellPreparing(Guid bakingProgramId, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                BakingProgram bakingProgram = _bakingProgramService.GetById(bakingProgramId);
                if (bakingProgram == null)
                    return NotFound();

                _bakingProgramService.CancellPreparing(bakingProgram);
                return Ok();
            }
            catch (BadProgramStatusException statusEx)
            {
                ModelState.AddModelError("ErrorToDisplay", statusEx.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut("finish-preparing/{bakingProgramId}")]
        public IActionResult FinishPreparing(Guid bakingProgramId, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                BakingProgram bakingProgram = _bakingProgramService.GetById(bakingProgramId);
                if (bakingProgram == null)
                    return NotFound();

                _bakingProgramService.FinishPreparing(bakingProgram);
                return Ok();
            }
             catch (BadProgramStatusException statusEx)
            {
                ModelState.AddModelError("ErrorToDisplay", statusEx.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("start-baking/{bakingProgramId}")]
        public IActionResult StartBaking(Guid bakingProgramId, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                BakingProgram bakingProgram = _bakingProgramService.GetById(bakingProgramId);
                if (bakingProgram == null)
                    return NotFound();

                bool isNextForBaking = _bakingProgramService.CheckIfProgramIsNextForBaking(bakingProgram);
                if (!isNextForBaking)
                {
                    ModelState.AddModelError("ErrorToDisplay", "Program is not next for baking, or oven is occupied by other program");
                    return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
                }

                _bakingProgramService.StartBaking(bakingProgram);
                return Ok();
            }   
           catch (BadProgramStatusException statusEx)
            {
                ModelState.AddModelError("ErrorToDisplay", statusEx.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("finish/{bakingProgramId}")]
        public IActionResult Finish(Guid bakingProgramId, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                BakingProgram bakingProgram = _bakingProgramService.GetById(bakingProgramId);
                if (bakingProgram == null)
                    return NotFound();

                _bakingProgramService.Finish(bakingProgram);
                return Ok();
            }
            catch (BadProgramStatusException statusEx)
            {
                ModelState.AddModelError("ErrorToDisplay", statusEx.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
