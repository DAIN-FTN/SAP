using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.StartPreparing;
using SAP_API.Mappers;
using SAP_API.Models;
using SAP_API.Repositories;
using System;
using System.Collections.Generic;

namespace SAP_API.Services
{
    public class StartPreparingService: IStartPreparingService
    {
        private readonly IReservedOrderProductRepository _reservedOrderProductRepository;
        private readonly IProductToPrepareRepository _productToPrepareRepository;
        Dictionary<Guid, List<ProductToPrepare>> productsToBePreparedGroupedByLocationDict = new Dictionary<Guid, List<ProductToPrepare>>();
        private BakingProgram bakingProgramToPrepare;



        public StartPreparingService(IReservedOrderProductRepository reservedOrderProductRepository, IProductToPrepareRepository productToPrepareRepository)
        {
            _reservedOrderProductRepository = reservedOrderProductRepository;
            _productToPrepareRepository = productToPrepareRepository;
        }
        public void SetProgramToPrepare(BakingProgram bakingProgram)
        {
            bakingProgramToPrepare = bakingProgram;
        }

        public void UseReservedProductsFromOrdersForPreparing()
        {
            List<BakingProgramProduct> productsFromProgram = bakingProgramToPrepare.Products;

            foreach (BakingProgramProduct productFromProgram in productsFromProgram)
            {
                Guid productToBePreparedId = productFromProgram.Product.Id;
                Guid orderInWhichProductWasOrderedId = productFromProgram.Order.Id;
                int quantityToBePrepared = productFromProgram.QuantityТoBake;
                List<ReservedOrderProduct> reservedProductQuantitiesOnLocations = GetInfoAboutReservedProductFromOrder(orderInWhichProductWasOrderedId, productToBePreparedId);

                CreateProductsToPrepare(reservedProductQuantitiesOnLocations, quantityToBePrepared);
            }
        }

        public StartPreparingResponse CreateStartPreparingResponse(BakingProgram bakingProgram)
        {
            List<ProductToPrepare> productsToPrepare = _productToPrepareRepository.GetByBakingProgramId(bakingProgram.Id);
            GroupProductsByLocation(productsToPrepare);

            List<LocationToPrepareFromResponse> preparingLocations = new List<LocationToPrepareFromResponse>();
            foreach (KeyValuePair<Guid, List<ProductToPrepare>> locationWithProducts in productsToBePreparedGroupedByLocationDict)
            {
                Guid locationId = locationWithProducts.Key;
                List<ProductToPrepare> products = locationWithProducts.Value;

                StockLocation location = products[0].LocationToPrepareFrom;

                List<ProductToPrepareResponse> productsToPrepareResponse = ProductToPrepareMapper.CreateProductsToPrepareResponse(products);

                LocationToPrepareFromResponse preparingLocation = new LocationToPrepareFromResponse
                {
                    LocationId = locationId,
                    LocationCode = location.Code,
                    Products = productsToPrepareResponse
                };
                preparingLocations.Add(preparingLocation);
            }

            StartPreparingResponse startPreparingResponse = new StartPreparingResponse
            {
                Id = bakingProgram.Id,
                Code = bakingProgram.Code,
                BakingProgrammedAt = bakingProgram.BakingProgrammedAt,
                OvenId = bakingProgram.Oven.Id,
                OvenCode = bakingProgram.Oven.Code,
                Locations = preparingLocations
            };

            return startPreparingResponse;

        }

        private void GroupProductsByLocation(List<ProductToPrepare> productsToPrepare)
        {
            foreach(ProductToPrepare product in productsToPrepare)
            {
                Guid locationToPrepareFromId = product.LocationToPrepareFrom.Id;
                if (!productsToBePreparedGroupedByLocationDict.ContainsKey(locationToPrepareFromId))
                {
                    productsToBePreparedGroupedByLocationDict[locationToPrepareFromId] = new List<ProductToPrepare>();
                }
                productsToBePreparedGroupedByLocationDict[locationToPrepareFromId].Add(product);
            }
           
        }

        private List<ReservedOrderProduct> GetInfoAboutReservedProductFromOrder(Guid orderId, Guid productId)
        {
            return _reservedOrderProductRepository.GetByOrderIdAndProductId(orderId, productId);
        }

        private void CreateProductsToPrepare(List<ReservedOrderProduct> reservedProductQuantitiesOnLocations, int quantityToBePrepared)
        {
            foreach (ReservedOrderProduct productReservedOnLocation in reservedProductQuantitiesOnLocations)
            {
                int quantityThatCanBePreparedFromLocation = productReservedOnLocation.GetReservedQuantityLeftForPreparing();
                int quantityToPrepareFromLocation = Math.Min(quantityThatCanBePreparedFromLocation, quantityToBePrepared);
                CreateProductToPrepare(productReservedOnLocation, quantityToPrepareFromLocation);

                quantityToBePrepared -= quantityToPrepareFromLocation;
                if (quantityToBePrepared == 0)
                {
                    break;
                }
            }
        }

        private void CreateProductToPrepare(ReservedOrderProduct productReservedOnLocation, int quantityToPrepareFromLocation)
        {
            ProductToPrepare productToPrepare = new ProductToPrepare
            {
                Product = productReservedOnLocation.Product,
                Order = productReservedOnLocation.Order,
                BakingProgram = bakingProgramToPrepare,
                LocationToPrepareFrom = productReservedOnLocation.LocationWhereProductIsReserved,
                QuantityToPrepare = quantityToPrepareFromLocation
            };

             _productToPrepareRepository.Create(productToPrepare);

        }

    }
}
