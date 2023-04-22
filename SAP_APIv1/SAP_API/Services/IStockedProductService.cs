using SAP_API.DTOs;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public interface IStockedProductService
    {
        public void ChangeStockOnLocationForProduct(Guid locationId, Guid productId, int quantityToSubstract);
        public bool IsThereEnoughStockForProducts(List<OrderProductRequest> orderProducts);
        public void reserveStockedProducts(List<OrderProductRequest> orderProducts);

        CreateStockedProductResponse Create(CreateStockedProductRequest stockedProduct);
    }
}
