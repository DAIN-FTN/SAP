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
        Dictionary<Guid, List<ProductToPrepare>> productsToBePreparedGroupedByLocationDict = new Dictionary<Guid, List<ProductToPrepare>>();


        public StartPreparingService(IReservedOrderProductRepository reservedOrderProductRepository)
        {
            _reservedOrderProductRepository = reservedOrderProductRepository;
        }

        public List<ReservedOrderProduct> GetInfoAboutReservedProductFromOrder(Guid orderId, Guid productId)
        {
            return _reservedOrderProductRepository.GetByOrderIdAndProductId(orderId, productId);
        }

        public StartPreparingResponse CreateStartPreparingResponse(BakingProgram bakingProgram)
        {
            List<LocationToPrepareFrom> preparingLocations = new List<LocationToPrepareFrom>();
            foreach (KeyValuePair<Guid, List<ProductToPrepare>> locationWithProducts in productsToBePreparedGroupedByLocationDict)
            {
                Guid locationId = locationWithProducts.Key;
                List<ProductToPrepare> products = locationWithProducts.Value;

                LocationToPrepareFrom preparingLocation = new LocationToPrepareFrom
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

        public void GroupProductsToBePreparedByLocation(List<ReservedOrderProduct> reservedProductQuantitiesOnLocations, int quantityToBePrepared)
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
                productsToBePreparedGroupedByLocationDict[locationWhereProductIsReservedId] = new List<ProductToPrepare>();
            }

            ProductToPrepare productToBePrepared = new ProductToPrepare
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
