using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAP_API.DTOs.Responses.StockLocation;
using SAP_API.Services;
using System;
using System.Collections.Generic;

namespace SAP_API.Controllers
{
    [ApiController]
    [Route("api/stock-locations")]
    public class StockLocationController: ControllerBase
    {
        private readonly IStockLocationService _stockLocationService;

        public StockLocationController(IStockLocationService stockLocationService)
        {
            _stockLocationService = stockLocationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<StockLocationResponse> response = _stockLocationService.GetAll();
                if (response == null || response.Count == 0)
                    return NoContent();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
