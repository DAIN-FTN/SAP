using SAP_API.DTOs.Responses;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Mappers
{
    public class StockedProductMapper
    {
        public static StockOnLocationResponse CreateStockOnLocationResponse(StockedProduct stockedProduct)
        {
            StockLocation location = stockedProduct.Location;
            return new StockOnLocationResponse
            {
                Id = location.Id,
                Code = location.Code,
                Quantity = stockedProduct.Quantity,
                ReservedQuantity = stockedProduct.ReservedQuantity
            };
        }
    }
}
