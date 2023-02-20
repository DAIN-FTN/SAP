using SAP_API.DTOs;
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

        public bool IsThereEnoughStockForProducts(List<OrderProductRequest> orderProducts)
        {
            foreach(OrderProductRequest orderProduct in orderProducts)
            {
                Guid productId = (Guid)orderProduct.ProductId;
                int quantity = (int)orderProduct.Quantity;

                if (!ThereIsEnoughStockForProduct(productId, quantity))
                    return false;
            }
            return true;
        }

        private bool ThereIsEnoughStockForProduct(Guid productId, int quantityToBake)
        {
            List<StockedProduct> stockedProducts = _stockedProductRepository.GetByProductId(productId);
            foreach(StockedProduct stockedProduct in stockedProducts)
            {
                int onHandQuantity = stockedProduct.Quantity - stockedProduct.ReservedQuantity;
                quantityToBake -= onHandQuantity;

                if (quantityToBake <= 0)
                    return true;
            }

            return quantityToBake <= 0;
        }

        public void reserveStockedProduct(Guid productId, int quantityToReserve)
        {
            /*
                1. Get stocked products
                2. We already checked if there is enough stock so next thing is to acctualy reserve this
                3. OrdeProduct should probably have a list of Locations where it is stored 
                4. If there is an issue we should roll this back [maybe]
                4. 
             */
           List<StockedProduct> stockedProducts = _stockedProductRepository.GetByProductId(productId);

            foreach (var stockedProduct in stockedProducts)
            {
                if(stockedProduct.ReservedQuantity >= quantityToReserve)
                {
                }
            }

        }
    }
}
