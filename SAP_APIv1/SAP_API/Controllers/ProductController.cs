using Microsoft.AspNetCore.Mvc;
using SAP_API.DTOs;
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

        [HttpGet("stock")]
        public ActionResult<ProductStockDTO> GetProductStock([FromQuery] String name)
        {
            List<ProductStockDTO> response = _productService.GetProductStock(name);

            if (response.Count == 0)
                return NoContent();

            return Ok(response);
        }
    }
}
