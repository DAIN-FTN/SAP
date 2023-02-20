using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.Services
{
    public interface IProductService
    {

        public List<ProductStockResponse> GetProductStock(string name);
        public ProductDetailsResponse GetProductDetails(Guid id);
        public List<ProductResponse> GetAll(string name);
        public Product GetById(Guid productId);
    }
}
