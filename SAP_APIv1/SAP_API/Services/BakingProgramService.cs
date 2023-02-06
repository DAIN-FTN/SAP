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

        public BakingProgramService(IProductRepository productRepository, IBakingProgramRepository bakingProgramRepository, IOvenRepository ovenRepository, IArrangingProductsToProgramsService arrangingService)
        {
            _bakingProgramRepository = bakingProgramRepository;
            _arrangingService = arrangingService;
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
