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

        //TODO get user from request
        public StartPreparingResponse GetDataForPreparing(Guid id)
        {
            BakingProgram bakingProgram = _bakingProgramRepository.GetById(id);
            _startPreparingService.SetProgramToPrepare(bakingProgram);

            if (bakingProgram.Status.Equals(BakingProgramStatus.Created))
            {
                _startPreparingService.UseReservedProductsFromOrdersForPreparing();
                bakingProgram.StartPreparing(new User {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Username = "Natalija",
                    Password = "pass123"
                });
            }

            return _startPreparingService.CreateStartPreparingResponse();

        }

        public void CreateBakingProgram(BakingProgram bakingProgram)
        {
            bakingProgram.Status = BakingProgramStatus.Created;
            _bakingProgramRepository.Create(bakingProgram);
        }

        public void UpdateBakingProgram(BakingProgram bakingProgram)
        {
           _bakingProgramRepository.Update(bakingProgram); 
        }

        public List<AvailableBakingProgramResponse> FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body)
        {
            ArrangingResult result = GetExistingOrNewProgramsProductShouldBeArrangedInto(body.ShouldBeDoneAt, body.OrderProducts);
            List<BakingProgram> listToMap = result.BakingPrograms;
            List<AvailableBakingProgramResponse> resultList = BakingProgramMapper.CreateListOfAvailableBakingProgramResponse(listToMap);
            return resultList;
        }

        public void FinishPreparing(Guid bakingProgramId)
        {
            List<ProductToPrepare> productsToPrepare = _productToPrepareRepository.GetByBakingProgramId(bakingProgramId);
            foreach (ProductToPrepare productToPrepare in productsToPrepare)
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

        //TODO test
        public bool CheckIfBakingProgramIsNextForPreparing(Guid bakingProgramId)
        {
            BakingProgram bakingProgram = _bakingProgramRepository.GetById(bakingProgramId);

            DateTime bakingProgrammedAtTime = bakingProgram.BakingProgrammedAt;
            DateTime timeNow = DateTime.Now;
            int numberOfMinutesBeforeProgrammedTimePreparingIsAllowed = 25;
            DateTime earliestTimeToStartPreparing = timeNow.AddMinutes(-numberOfMinutesBeforeProgrammedTimePreparingIsAllowed);

            if (bakingProgrammedAtTime < earliestTimeToStartPreparing)
                return false;

            List<BakingProgram> bakingProgramsThatShouldBePrepared = _bakingProgramRepository.GetProgramsWithBakingProgrammedAtBetweenDateTimes(earliestTimeToStartPreparing, timeNow.AddMinutes(numberOfMinutesBeforeProgrammedTimePreparingIsAllowed));
            if (bakingProgramsThatShouldBePrepared.Count == 0)
                return false;

            SortBakingProgramsByProgrammedAtTime(bakingProgramsThatShouldBePrepared);

            Guid ovenId = bakingProgram.Oven.Id;
            List<BakingProgram> programsProgrammedForOven = bakingProgramsThatShouldBePrepared.FindAll(bp => bp.Oven.Id == ovenId);
            if (programsProgrammedForOven.Count == 0)
                return false;

            BakingProgram bakingProgramThatShouldBeNextPreparedForOven = programsProgrammedForOven[0];
            return bakingProgramThatShouldBeNextPreparedForOven.Id == bakingProgramId;
        }

        private void SortBakingProgramsByProgrammedAtTime(List<BakingProgram> bakingPrograms)
        {
            bakingPrograms.Sort((bp1, bp2) => bp1.BakingProgrammedAt.CompareTo(bp2.BakingProgrammedAt));
        }

        public void CancellPreparing(Guid bakingProgramId)
        {
            BakingProgram bakingProgram = _bakingProgramRepository.GetById(bakingProgramId);
            bakingProgram.CancellPreparing();
            //TODO SaveChanges
        }

        //TODO user from req
        public AllBakingProgramsResponse GetBakingProgramsForUser()
        {
            User user = new User
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Username = "Natalija",
                Password = "pass123"
            };

            DateTime timeNow = DateTime.Now;
            DateTime startTime = timeNow.AddDays(-1);
            DateTime endTime = timeNow.AddHours(4);
            List<BakingProgram> bakingPrograms = _bakingProgramRepository.GetProgramsWithBakingProgrammedAtBetweenDateTimes(startTime, endTime);
            List<BakingProgram> programsUserIsPreparing = bakingPrograms.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Preparing) && bp.PreparedBy.Id == user.Id);

            if (programsUserIsPreparing.Count == 0)
                return BakingProgramMapper.CreateAllBakingProgramsResponse(bakingPrograms, null);

            BakingProgram programUserIsPreparing = programsUserIsPreparing[0];
            AllBakingProgramsResponse response = BakingProgramMapper.CreateAllBakingProgramsResponse(bakingPrograms, programUserIsPreparing);
            StartPreparingResponse preparingInProgressResponse = GetDataForPreparing(programUserIsPreparing.Id);
            response.PreparingInProgress = preparingInProgressResponse;
            return response;

        }

        public bool CheckIfProgramIsNextForBaking(Guid bakingProgramId)
        {
            BakingProgram bakingProgram = _bakingProgramRepository.GetById(bakingProgramId);

            DateTime bakingProgrammedAtTime = bakingProgram.BakingProgrammedAt;
            DateTime timeNow = DateTime.Now;
            int numberOfMinutesBeforeProgrammedTimeBakingIsAllowed = 5;
            DateTime earliestTimeToStartBaking = timeNow.AddMinutes(-numberOfMinutesBeforeProgrammedTimeBakingIsAllowed);

            if (bakingProgrammedAtTime < earliestTimeToStartBaking)
                return false;

            List<BakingProgram> bakingProgramsThatShouldBeBaked = _bakingProgramRepository.GetProgramsWithBakingProgrammedAtBetweenDateTimes(earliestTimeToStartBaking, timeNow.AddMinutes(numberOfMinutesBeforeProgrammedTimeBakingIsAllowed));
            if (bakingProgramsThatShouldBeBaked.Count == 0)
                return false;

            SortBakingProgramsByProgrammedAtTime(bakingProgramsThatShouldBeBaked);

            Guid ovenId = bakingProgram.Oven.Id;
            List<BakingProgram> programsProgrammedForOven = bakingProgramsThatShouldBeBaked.FindAll(bp => bp.Oven.Id == ovenId);
            if (programsProgrammedForOven.Count == 0)
                return false;

            List<BakingProgram> preparedProgramsForOven = programsProgrammedForOven.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Prepared));
            BakingProgram bakingProgramThatShouldBeNextBaked = preparedProgramsForOven[0];
            return bakingProgramThatShouldBeNextBaked.Id == bakingProgramId && OvenIsNotOccupiedByOtherProgram(ovenId);
        }

        private bool OvenIsNotOccupiedByOtherProgram(Guid ovenId)
        {
            DateTime timeNow = DateTime.Now;
            DateTime startTime = timeNow.AddDays(-1);
            int numberOfMinutesBeforeProgrammedTimeBakingIsAllowed = 5;
            List<BakingProgram> bakingPrograms = _bakingProgramRepository.GetProgramsWithBakingProgrammedAtBetweenDateTimes(startTime, timeNow.AddMinutes(numberOfMinutesBeforeProgrammedTimeBakingIsAllowed));
            List<BakingProgram> programsProgrammedForOven = bakingPrograms.FindAll(bp => bp.Oven.Id == ovenId);
            List<BakingProgram>  programsBakingOrDoneInOven = programsProgrammedForOven.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Baking) || bp.Status.Equals(BakingProgramStatus.Done));
            return programsBakingOrDoneInOven.Count == 0;
        }

        public void StartBaking(Guid bakingProgramId)
        {
            BakingProgram bakingProgram = _bakingProgramRepository.GetById(bakingProgramId);
            bakingProgram.StartBaking();
            //TODO save changes
        }
    }
}
