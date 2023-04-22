using SAP_API.DTOs.Requests;
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

        public static StockedProduct CreateStockedProductFromCreateStockedProductRequest(CreateStockedProductRequest body)
        {
            return new StockedProduct
            {
                Id = Guid.NewGuid(),
                LocationId = body.LocationId,
                ProductId = body.ProductId,
                Quantity = body.Quantity,
                ReservedQuantity = 0
            };
        }

        public static CreateStockedProductResponse CreateCreateStockedProductResponseFromStockedProduct(StockedProduct body)
        {
            return new CreateStockedProductResponse
            {
                LocationId = body.LocationId,
                LocationCode = body.Location.Code,
                ProductId = body.ProductId,
                ProductName = body.Product.Name,
                Quantity = body.Quantity,
                ReservedQuantity = body.ReservedQuantity
            };
        }


    }
}
