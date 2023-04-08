using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SAP_API.DTOs;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Exceptions;
using SAP_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderTransactionsOrchestrator _orderCreationOrchestrator;

        public OrderController(IOrderTransactionsOrchestrator orderCreationOrchestrator)
        {
            _orderCreationOrchestrator = orderCreationOrchestrator;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest body, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                CreateOrderResponse response = _orderCreationOrchestrator.OrchestrateOrderCreation(body);
                return Ok(response);
            }
            catch(OrderCreationException ex)
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
