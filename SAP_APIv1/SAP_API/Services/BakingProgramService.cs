using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
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
        private readonly IStockedProductService _stockedProductService;
        private readonly IProductToPrepareRepository _productToPrepareRepository;


        public BakingProgramService(IBakingProgramRepository bakingProgramRepository, IArrangingProductsToProgramsService arrangingService, IStartPreparingService startPreparingService, IStockedProductService stockedProductService, IProductToPrepareRepository productToPrepareRepository)
        {
            _bakingProgramRepository = bakingProgramRepository;
            _arrangingService = arrangingService;
            _startPreparingService = startPreparingService;
            _stockedProductService = stockedProductService;
            _productToPrepareRepository = productToPrepareRepository;
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
            _startPreparingService.SetProgramToPrepare(bakingProgram);

            if (bakingProgram.Status.Equals(BakingProgramStatus.Created))
            {
                _startPreparingService.UseReservedProductsFromOrdersForPreparing();
                bakingProgram.StartPreparing();
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

        public void FinishPreparing(Guid bakingProgramId)
        {
            List<ProductToPrepare> productsToPrepare = _productToPrepareRepository.GetByBakingProgramId(bakingProgramId);
            foreach(ProductToPrepare productToPrepare in productsToPrepare)
            {
                Guid locationForStockChange = productToPrepare.LocationToPrepareFrom.Id;
                Guid productForStockChange = productToPrepare.Product.Id;
                int quantityToSubstract = productToPrepare.QuantityToPrepare;
                _stockedProductService.ChangeStockOnLocationForProduct(locationForStockChange, productForStockChange, quantityToSubstract);
                
            }

            BakingProgram bakingProgram = _bakingProgramRepository.GetById(bakingProgramId);
            bakingProgram.FinishPreparing();
            //TODO saveChanges

        }
    }
}
