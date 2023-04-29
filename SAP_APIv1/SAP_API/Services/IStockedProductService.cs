using SAP_API.DTOs;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.StockedProduct;
using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.Services
{
    public interface IStockedProductService
    {
        public void ChangeStockOnLocationForProduct(Guid locationId, Guid productId, int quantityToSubstract);
        public bool IsThereEnoughStockForProducts(List<OrderProductRequest> orderProducts);

        CreateStockedProductResponse Create(CreateStockedProductRequest stockedProduct);
        UpdateStockedProductResponse Update(StockedProduct stockedProduct, UpdateStockedProductRequest body);
        StockedProduct GetByLocationIdProductId(Guid locationId, Guid productId);

        public List<ReservedOrderProduct> reserveStockedProducts(List<OrderProductRequest> orderProducts, Guid orderId);

    }
}
