using Microsoft.AspNetCore.Mvc;
using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
using SAP_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost]
        public ActionResult<List<BakingProgramResponse>> CheckAvailableBakingPrograms([FromBody] FindAvailableBakingProgramsRequest body)
        {
            List<BakingProgramResponse> response = _bakingProgramService.FindAvailableBakingPrograms(body);
            if (response.Count == 0)
                return NoContent();
            return Ok(response);
        }
    }
}
