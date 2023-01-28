using SAP_API.DTOs;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Mappers
{
    public class ProductMapper
    {
        public static ProductStockResponse CreateProductStockDTO(Product product, int availableQuantity)
        {
            return new ProductStockResponse
            {
                Id = product.Id,
                Name = product.Name,
                AvailableQuantity = availableQuantity
            };
        }
    }
}
