
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.StockedProduct;
using SAP_API.Models;
using SAP_API.Services;
using SAP_API.Validation;
using System;

namespace SAP_API.Controllers
{
    [ApiController]
    [Route("api/stocked-products")]
    [Authorize]
    public class StockedProductController: ControllerBase
    {
        private readonly IStockedProductService _stockedProductService;

        public StockedProductController(IStockedProductService stockedProductService)
        {
            _stockedProductService = stockedProductService;
        }

        [HttpPost]
        public IActionResult CreateStockedProduct([FromBody] CreateStockedProductRequest body, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                if (body.ProductId == null)
                {
                    ModelState.AddModelError("ErrorToDisplay", "ProductId must be provided.");
                    return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
                }

                CustomValidationResult validationResult = body.IsValid();
                if (!validationResult.Success)
                {
                    ModelState.AddModelError("ErrorToDisplay", validationResult.ErrorMessage);
                    return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
                }


                CreateStockedProductResponse response = _stockedProductService.Create(body);
                return Ok(response);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("ErrorToDisplay", ex.InnerException.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateStockedProduct([FromBody] UpdateStockedProductRequest body, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                StockedProduct stockedProduct = _stockedProductService.GetByLocationIdProductId((Guid)body.LocationId, (Guid)body.ProductId);
                if (stockedProduct == null)
                    return NotFound();

                CustomValidationResult validationResult = body.IsValid();
                if (!validationResult.Success)
                {
                    ModelState.AddModelError("ErrorToDisplay", validationResult.ErrorMessage);
                    return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
                }

                UpdateStockedProductResponse response = _stockedProductService.Update(stockedProduct, body);
                return Ok(response);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("ErrorToDisplay", ex.InnerException.Message);
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
