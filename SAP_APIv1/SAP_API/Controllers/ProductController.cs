using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAP_API.DTOs;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Models;
using SAP_API.Services;
using System;
using System.Collections.Generic;

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

        public IActionResult CreateProduct([FromBody] CreateProductRequest body)
        {
            try
            {
                CreateProductResponse response = _productService.CreateProduct(body);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
