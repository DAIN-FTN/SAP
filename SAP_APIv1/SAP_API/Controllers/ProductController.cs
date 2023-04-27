using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SAP_API.DTOs;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Models;
using SAP_API.Services;
using SAP_API.Validation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SAP_API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string name)
        {
            try
            {
                List<ProductResponse> response = _productService.GetAll(name);
                if (response == null || response.Count == 0)
                    return NoContent();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{productId}")]
        public IActionResult GetDetails(Guid productId)
        {
            try
            {
                Product product = _productService.GetById(productId);
                if (product == null)
                    return NotFound();

                ProductDetailsResponse response = _productService.GetProductDetails(productId);

                if (response == null)
                    return NoContent();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("stock")]
        public IActionResult GetProductStock([FromQuery] String name)
        {
            try
            {
                List<ProductStockResponse> response = _productService.GetProductStock(name);

                if (response.Count == 0)
                    return NoContent();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductRequest body, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                CustomValidationResult validationResult = body.IsValid();
                if (!validationResult.Success)
                {
                    ModelState.AddModelError("ErrorToDisplay", validationResult.ErrorMessage);
                    return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
                }
                CreateProductResponse response = _productService.CreateProduct(body);
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

        [HttpPut("{productId}")]
        public IActionResult UpdateProduct([FromBody] UpdateProductRequest body, Guid productId, [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            try
            {
                Product product = _productService.GetById(productId);
                if (product == null)
                    return NotFound();

                CustomValidationResult validationResult = body.IsValid();
                if (!validationResult.Success)
                {
                    ModelState.AddModelError("ErrorToDisplay", validationResult.ErrorMessage);
                    return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
                }

                UpdateProductResponse response = _productService.UpdateProduct(product, body);
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
