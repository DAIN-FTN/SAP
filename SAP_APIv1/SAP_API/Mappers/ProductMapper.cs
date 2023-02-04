using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
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

        public static ProductDetailsResponse CreateProductDetailsResponse(List<StockedProduct> stockDetails, Product productDetails)
        {
            List<StockOnLocationResponse> stockOnLocations = new List<StockOnLocationResponse>();

            foreach (StockedProduct stock in stockDetails)
            {
                stockOnLocations.Add(StockedProductMapper.CreateStockOnLocationResponse(stock));
            }

            return new ProductDetailsResponse
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                BakingTimeInMins = productDetails.BakingTimeInMins,
                BakingTempInC = productDetails.BakingTempInC,
                Size = productDetails.Size,
                LocationsWithStock = stockOnLocations
            };
        }

        
    }
}
