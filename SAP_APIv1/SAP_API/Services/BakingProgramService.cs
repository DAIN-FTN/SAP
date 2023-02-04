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

        private readonly IArrangingProductsToProgramsService _arrangingService;
        private readonly IStartPreparingService _startPreparingService;


        public BakingProgramService(IBakingProgramRepository bakingProgramRepository, IArrangingProductsToProgramsService arrangingService, IStartPreparingService startPreparingService)
        {
            _bakingProgramRepository = bakingProgramRepository;
            _arrangingService = arrangingService;
            _startPreparingService = startPreparingService;
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

        public StartPreparingResponse GetDataForPreparing(Guid id)
        {
            BakingProgram bakingProgram = _bakingProgramRepository.GetById(id);
            List<BakingProgramProduct> productsFromProgram = bakingProgram.Products;

            foreach(BakingProgramProduct productFromProgram in productsFromProgram)
            {
                Guid productToBePreparedId = productFromProgram.Product.Id;
                Guid orderInWhichProductWasOrderedId = productFromProgram.Order.Id;
                int quantityToBePrepared = productFromProgram.QuantityТoBake;
                List<ReservedOrderProduct> reservedProductQuantitiesOnLocations = _startPreparingService.GetInfoAboutReservedProductFromOrder(orderInWhichProductWasOrderedId, productToBePreparedId);

                _startPreparingService.GroupProductsToBePreparedByLocation(reservedProductQuantitiesOnLocations, quantityToBePrepared);
            }

            return _startPreparingService.CreateStartPreparingResponse(bakingProgram);

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
