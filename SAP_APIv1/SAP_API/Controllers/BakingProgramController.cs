using Microsoft.AspNetCore.Mvc;
using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
using SAP_API.Models;
using SAP_API.Services;
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

        [HttpPost("available")]
        public ActionResult<List<BakingProgramResponse>> FindAvailableBakingPrograms([FromBody] FindAvailableBakingProgramsRequest body)
        {
           
           /*return new List<BakingProgramResponse>
            {
                new BakingProgramResponse
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000000"),
                    Code = "Bake-0001",
                    CreatedAt = new DateTime(2022, 12, 31, 23, 59, 59),
                    Status = "Created",
                    BakingTimeInMins = 60,
                    BakingTempInC = 180,
                    BakingProgrammedAt = new DateTime(2023, 1, 1, 0, 0, 0),
                    BakingStartedAt = null,
                    OvenCode = "Oven-A"
                },
                new BakingProgramResponse
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Code = "SisajGa-0002",
                    CreatedAt = new DateTime(2022, 12, 31, 23, 59, 59),
                    Status = "Created",
                    BakingTimeInMins = 120,
                    BakingTempInC = 200,
                    BakingProgrammedAt = new DateTime(2023, 1, 1, 0, 0, 0),
                    BakingStartedAt = null,
                    OvenCode = "Oven-B"
                }
            };*/

            
            List<BakingProgramResponse> response = _bakingProgramService.FindAvailableBakingPrograms(body);
            if (response.Count == 0)
                return NoContent();
            return Ok(response);
            
        }

        [HttpGet("start-preparing/{bakingProgramId}")]
        public ActionResult<StartPreparingResponse> StartPreparing(Guid bakingProgramId)
        {
            //TODO check if proram is next for preparing
           bool isNextForPreparing = _bakingProgramService.CheckIfBakingProgramIsNextForPreparing(bakingProgramId);
           /*if (!isNextForPreparing)
                return Ok("Program is not next for preparing");*/
            StartPreparingResponse response = _bakingProgramService.GetDataForPreparing(bakingProgramId);
            return Ok(response);
        }

        [HttpPut("finish-preparing/{bakingProgramId}")]
        public ActionResult<StartPreparingResponse> FinishPreparing(Guid bakingProgramId)
        {
            _bakingProgramService.FinishPreparing(bakingProgramId);
            return Ok();
        }

    }
}
