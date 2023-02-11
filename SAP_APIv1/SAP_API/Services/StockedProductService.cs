using SAP_API.Models;
using SAP_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public class StockedProductService : IStockedProductService
    {
        private readonly IStockedProductRepository _stockedProductRepository;

        public StockedProductService(IStockedProductRepository stockedProductRepository)
        {
            _stockedProductRepository = stockedProductRepository;
        }

        public void ChangeStockOnLocationForProduct(Guid locationId, Guid productId, int quantityToSubstract)
        {
            StockedProduct stockedProduct = _stockedProductRepository.GetByLocationAndProduct(locationId, productId);
            stockedProduct.Quantity -= quantityToSubstract;
            stockedProduct.ReservedQuantity -= quantityToSubstract;
            //TODO saveChanges
        }
    }
}
