using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.StartPreparing;
using SAP_API.Mappers;
using SAP_API.Models;
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

            bool programHadPreparingStatus = CheckIfBakingProgramHadPreparingStatus();
            if (programHadPreparingStatus)
                return;

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
        /// <summary>
        /// Checks if there are already products to prepare records for a program.
        /// If program was chosen by some user to be prepared, status is changed to prepared 
        /// and products to prepare are recorded. If the user then cancells preparing, products
        /// to prepare record stay recorded, but program has a created status.
        /// </summary>
        /// <returns>true if program was previously in prepared status</returns>
        private bool CheckIfBakingProgramHadPreparingStatus()
        {
            List<ProductToPrepare> productsToPrepare = _productToPrepareRepository.GetByBakingProgramId(bakingProgramToPrepare.Id);
            return productsToPrepare.Count != 0;
        }

        public StartPreparingResponse CreateStartPreparingResponse()
        {
            List<ProductToPrepare> productsToPrepare = _productToPrepareRepository.GetByBakingProgramId(bakingProgramToPrepare.Id);
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
                Id = bakingProgramToPrepare.Id,
                Code = bakingProgramToPrepare.Code,
                BakingProgrammedAt = bakingProgramToPrepare.BakingProgrammedAt,
                OvenId = bakingProgramToPrepare.Oven.Id,
                OvenCode = bakingProgramToPrepare.Oven.Code,
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

            //TODO SaveChanges
             _productToPrepareRepository.Create(productToPrepare);

        }

    }
}
