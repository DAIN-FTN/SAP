using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.StartPreparing;
using SAP_API.Mappers;
using SAP_API.Models;
using SAP_API.Repositories;
using System;
using System.Collections.Generic;

namespace SAP_API.Services
{
    public class BakingProgramService : IBakingProgramService
    {
        private readonly IBakingProgramRepository _bakingProgramRepository;
        private readonly IOrderProductRepository _orderProductRepository;

        private readonly IArrangingProductsToProgramsService _arrangingService;

        public BakingProgramService(IProductRepository productRepository, IBakingProgramRepository bakingProgramRepository, IOvenRepository ovenRepository, IArrangingProductsToProgramsService arrangingService, IOrderProductRepository orderProductRepository)
        {
            _bakingProgramRepository = bakingProgramRepository;
            _arrangingService = arrangingService;
            _orderProductRepository = orderProductRepository;
        }


        public ArrangingResult GetExistingOrNewProgramsProductShouldBeArrangedInto(DateTime timeOrderShouldBeDone, List<OrderProductRequest> orderProducts)
        {
            List<BakingProgram> programsProductsShouldBeArrangedTo = new List<BakingProgram>();

            _arrangingService.SetTimeOrderShouldBeDone(timeOrderShouldBeDone);
            _arrangingService.PrepareProductsForArranging(orderProducts);

            List<BakingProgram> existingPrograms = _arrangingService.GetExistingProgramsProductShouldBeArrangedInto();
            programsProductsShouldBeArrangedTo.AddRange(existingPrograms);

            bool allProductsSuccessfullyArranged = !_arrangingService.ThereAreProductsLeftForArranging();
            if (allProductsSuccessfullyArranged)
            {
                return new ArrangingResult { 
                    BakingPrograms = programsProductsShouldBeArrangedTo,
                    AllProductsCanBeSuccessfullyArranged = true
                };
            }

            List<BakingProgram> newPrograms = _arrangingService.GetNewProgramsProductsShouldBeArrangedInto();
            programsProductsShouldBeArrangedTo.AddRange(newPrograms);

            allProductsSuccessfullyArranged = !_arrangingService.ThereAreProductsLeftForArranging();
            return new ArrangingResult
            {
                BakingPrograms = programsProductsShouldBeArrangedTo,
                AllProductsCanBeSuccessfullyArranged = allProductsSuccessfullyArranged
            };

        }

        public StartPreparingResponse StartPreparingProgram(Guid id)
        {
            BakingProgram bakingProgram = _bakingProgramRepository.GetById(id);
            List<BakingProgramProduct> productsFromProgram = bakingProgram.Products;
            Dictionary<Guid, List<StartPreparingProductFromOrderResponse>> productsToBePreparedFromLocationDict = new Dictionary<Guid, List<StartPreparingProductFromOrderResponse>>();

            foreach(BakingProgramProduct productFromProgram in productsFromProgram)
            {
                Guid productId = productFromProgram.Product.Id;
                Guid orderId = productFromProgram.Order.Id;
                int quantityToBePrepared = productFromProgram.QuantityТoBake;
                List<OrderProduct> reservedProductQuantitesFromOrder = _orderProductRepository.GetByOrderIdAndProductId(orderId, productId);

                AddProductsToLocationsFromWhichProductsShouldBePrepared(reservedProductQuantitesFromOrder, quantityToBePrepared, productsToBePreparedFromLocationDict);
            }

            return CreateStartPreparingResponse(productsToBePreparedFromLocationDict, bakingProgram);

        }

        private StartPreparingResponse CreateStartPreparingResponse(Dictionary<Guid, List<StartPreparingProductFromOrderResponse>> productsToBePreparedFromLocationDict, BakingProgram bakingProgram)
        {
            List<StartPreparingFromLocationResponse> preparingLocations = new List<StartPreparingFromLocationResponse>();
            foreach (KeyValuePair<Guid, List<StartPreparingProductFromOrderResponse>> locationWithProducts in productsToBePreparedFromLocationDict)
            {
                Guid locationId = locationWithProducts.Key;
                List<StartPreparingProductFromOrderResponse> products = locationWithProducts.Value;

                StartPreparingFromLocationResponse preparingLocation = new StartPreparingFromLocationResponse
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

        private void AddProductsToLocationsFromWhichProductsShouldBePrepared(List<OrderProduct> reservedProductQuantitesFromOrder, int quantityToBePrepared, Dictionary<Guid, List<StartPreparingProductFromOrderResponse>> productsToBePreparedFromLocationDict)
        {
            foreach(OrderProduct reservedProduct in reservedProductQuantitesFromOrder)
            {
                int reservedQuantity = reservedProduct.GetReservedQuantityLeftForPreparing();
                int quantityToPrepareFromLocation = Math.Min(reservedQuantity, quantityToBePrepared);
                AddProductToLocationForPreparing(reservedProduct, productsToBePreparedFromLocationDict, quantityToPrepareFromLocation);

                quantityToBePrepared -= quantityToPrepareFromLocation;
                if (quantityToBePrepared == 0)
                {
                    break;
                }
            }
        }


        private void AddProductToLocationForPreparing(OrderProduct reservedProduct, Dictionary<Guid, List<StartPreparingProductFromOrderResponse>> productsToBePreparedFromLocationDict, int quantityToPrepareFromLocation)
        {
            Guid idOfLocationWhereProductIsReserved = reservedProduct.Location.Id;
            if (!productsToBePreparedFromLocationDict.ContainsKey(idOfLocationWhereProductIsReserved))
            {
                productsToBePreparedFromLocationDict[idOfLocationWhereProductIsReserved] = new List<StartPreparingProductFromOrderResponse>();
            }

            StartPreparingProductFromOrderResponse productToBePrepared = new StartPreparingProductFromOrderResponse
            {
                ProductId = reservedProduct.Product.Id,
                Name = reservedProduct.Product.Name,
                OrderId = reservedProduct.Order.Id,
                Quantity = quantityToPrepareFromLocation
            };
            productsToBePreparedFromLocationDict[idOfLocationWhereProductIsReserved].Add(productToBePrepared);

        }

        public void CreateBakingProgram()
        {
            throw new NotImplementedException();
        }

        public void UpdateBakingProgram(BakingProgram bakingProgram)
        {
            throw new NotImplementedException();
        }

        public List<BakingProgramResponse> FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body)
        {
            ArrangingResult result = GetExistingOrNewProgramsProductShouldBeArrangedInto(body.ShouldBeDoneAt, body.OrderProducts);
            List<BakingProgram> listToMap = result.BakingPrograms;
            List<BakingProgramResponse> resultList = BakingProgramMapper.CreateListOfBakingProgramResponse(listToMap);
            return resultList;
        }
    }
}
