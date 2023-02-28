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

        public static ProductResponse CreateProductResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name
            };
        }

        public static List<ProductResponse> CreateListOfProductResponse(List<Product> products)
        {
            List<ProductResponse> response = new List<ProductResponse>();
            foreach (Product product in products)
            {
                response.Add(CreateProductResponse(product));
            }

            return response;
        }
        
        
        public static ReservedOrderProduct OrderProductRequestToReservedOrderProduct(OrderProductRequest orderProductRequest, Order order, Product product)
        {
            return new ReservedOrderProduct
            {
                Id = Guid.NewGuid(),
                Order = order,
                Product = product,
                //TODO: check if this cast can be handled differently int? -> int
                ReservedQuantity = (int)orderProductRequest.Quantity
            };
        }
    }
}
