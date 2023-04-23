using SAP_API.DTOs;
using SAP_API.Models;
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
        public List<ReservedOrderProduct> reserveStockedProducts(List<OrderProductRequest> orderProducts, Guid orderId);
    }
}
