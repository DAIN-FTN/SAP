using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SAP_API.DTOs;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.Order;
using SAP_API.Exceptions;
using SAP_API.Models;
using SAP_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderTransactionsOrchestrator _orderCreationOrchestrator;
        private readonly IOrderService _orderService;

        public OrderController(IOrderTransactionsOrchestrator orderCreationOrchestrator, IOrderService orderService)
        {
            _orderCreationOrchestrator = orderCreationOrchestrator;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                List<OrderResponse> response = _orderService.GetOrders();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{orderId}")]
        public IActionResult GetById(Guid orderId)
        {
            try
            {
                Order order = _orderService.GetById(orderId);
                if(order == null)
                    return NotFound();

                OrderDetailsResponse response = _orderService.GetOrderDetails(order);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
