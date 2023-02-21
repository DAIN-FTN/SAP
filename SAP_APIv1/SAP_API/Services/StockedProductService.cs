using SAP_API.DTOs;
using SAP_API.Exceptions;
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

        public void reserveStockedProducts(List<OrderProductRequest> orderProducts)
        {
            if(this.IsThereEnoughStockForProducts(orderProducts) == false)
            {
                throw new NotEnoughStockedProductException();
            }

            foreach (OrderProductRequest orderProduct in orderProducts)
            {
                int quantityToReserve = orderProduct.Quantity.Value;
                List<StockedProduct> stockedProducts = _stockedProductRepository.GetByProductId(orderProduct.ProductId.Value);

                foreach (var stockedProduct in stockedProducts)
                {
                    if(quantityToReserve == 0) break;

                    if (stockedProduct.GetAvailableQuantity() == 0) continue;
                    
                    if (stockedProduct.GetAvailableQuantity() >= quantityToReserve)
                    {
                        stockedProduct.ReservedQuantity += quantityToReserve;
                        quantityToReserve= 0;
                    } else
                    {
                        //reserve anything that remains
                        quantityToReserve -= stockedProduct.GetAvailableQuantity();
                        stockedProduct.ReservedQuantity += stockedProduct.GetAvailableQuantity();
                    }
                    _stockedProductRepository.Update(stockedProduct);
                }
            }

        }
    }
}
