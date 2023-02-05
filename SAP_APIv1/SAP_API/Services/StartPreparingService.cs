using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.StartPreparing;
using SAP_API.Models;
using SAP_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public class StartPreparingService: IStartPreparingService
    {
        private readonly IReservedOrderProductRepository _reservedOrderProductRepository;
        Dictionary<Guid, List<ProductToPrepareResponse>> productsToBePreparedGroupedByLocationDict = new Dictionary<Guid, List<ProductToPrepareResponse>>();
        private BakingProgram bakingProgramToPrepare;



        public StartPreparingService(IReservedOrderProductRepository reservedOrderProductRepository)
        {
            _reservedOrderProductRepository = reservedOrderProductRepository;
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

                GroupProductsToBePreparedByLocation(reservedProductQuantitiesOnLocations, quantityToBePrepared);
            }
        }

        public StartPreparingResponse CreateStartPreparingResponse(BakingProgram bakingProgram)
        {
            List<LocationToPrepareFromResponse> preparingLocations = new List<LocationToPrepareFromResponse>();
            foreach (KeyValuePair<Guid, List<ProductToPrepareResponse>> locationWithProducts in productsToBePreparedGroupedByLocationDict)
            {
                Guid locationId = locationWithProducts.Key;
                List<ProductToPrepareResponse> products = locationWithProducts.Value;

                LocationToPrepareFromResponse preparingLocation = new LocationToPrepareFromResponse
                {
                    LocationId = locationId,
                    LocationCode = "",
                    Products = products
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

        private List<ReservedOrderProduct> GetInfoAboutReservedProductFromOrder(Guid orderId, Guid productId)
        {
            return _reservedOrderProductRepository.GetByOrderIdAndProductId(orderId, productId);
        }

        private void GroupProductsToBePreparedByLocation(List<ReservedOrderProduct> reservedProductQuantitiesOnLocations, int quantityToBePrepared)
        {
            foreach (ReservedOrderProduct productReservedOnLocation in reservedProductQuantitiesOnLocations)
            {
                int quantityThatCanBePreparedFromLocation = productReservedOnLocation.GetReservedQuantityLeftForPreparing();
                int quantityToPrepareFromLocation = Math.Min(quantityThatCanBePreparedFromLocation, quantityToBePrepared);
                GroupProductToLocationForPreparing(productReservedOnLocation, quantityToPrepareFromLocation);

                quantityToBePrepared -= quantityToPrepareFromLocation;
                if (quantityToBePrepared == 0)
                {
                    break;
                }
            }
        }

        private void GroupProductToLocationForPreparing(ReservedOrderProduct productReservedOnLocation, int quantityToPrepareFromLocation)
        {
            Guid locationWhereProductIsReservedId = productReservedOnLocation.LocationWhereProductIsReserved.Id;
            if (!productsToBePreparedGroupedByLocationDict.ContainsKey(locationWhereProductIsReservedId))
            {
                productsToBePreparedGroupedByLocationDict[locationWhereProductIsReservedId] = new List<ProductToPrepareResponse>();
            }

            ProductToPrepareResponse productToBePrepared = new ProductToPrepareResponse
            {
                ProductId = productReservedOnLocation.Product.Id,
                Name = productReservedOnLocation.Product.Name,
                OrderId = productReservedOnLocation.Order.Id,
                Quantity = quantityToPrepareFromLocation
            };
            productsToBePreparedGroupedByLocationDict[locationWhereProductIsReservedId].Add(productToBePrepared);

        }

    }
}
