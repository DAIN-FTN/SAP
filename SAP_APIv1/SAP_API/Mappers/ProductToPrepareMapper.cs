using SAP_API.DTOs.Responses.StartPreparing;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Mappers
{
    public class ProductToPrepareMapper
    {
        public static ProductToPrepareResponse CreateProductToPrepareResponse(ProductToPrepare productToPrepare)
        {
            return new ProductToPrepareResponse 
            {
                ProductId = productToPrepare.Product.Id,
                Name = productToPrepare.Product.Name,
                OrderId = productToPrepare.Order.Id,
                Quantity = productToPrepare.QuantityToPrepare
            };
        }

        public static List<ProductToPrepareResponse> CreateProductsToPrepareResponse(List<ProductToPrepare> productsToPrepare)
        {
            List<ProductToPrepareResponse> productsToPrepareResponse = new List<ProductToPrepareResponse>();

            foreach(ProductToPrepare product in productsToPrepare)
            {
                productsToPrepareResponse.Add(ProductToPrepareMapper.CreateProductToPrepareResponse(product));
            }

            return productsToPrepareResponse;
        }
    }
}
