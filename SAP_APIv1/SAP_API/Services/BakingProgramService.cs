using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Mappers;
using SAP_API.Models;
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


        public ArrangingResult GetExistingOrNewProgramsProductShouldBeArrangedInto(DateTime timeOrderShouldBeDone, List<OrderProductRequest> orderProducts, Guid orderId)
        {

            bool thereIsEnoughStock = _stockedProductService.IsThereEnoughStockForProducts(orderProducts);

            List<BakingProgram> programsProductsShouldBeArrangedTo = new List<BakingProgram>();

            _arrangingService.SetTimeOrderShouldBeDone(timeOrderShouldBeDone);
            _arrangingService.PrepareProductsForArranging(orderProducts);

            List<BakingProgram> existingPrograms = _arrangingService.GetExistingProgramsProductShouldBeArrangedInto(orderId);
            programsProductsShouldBeArrangedTo.AddRange(existingPrograms);

            bool allProductsSuccessfullyArranged = !_arrangingService.ThereAreProductsLeftForArranging();
            if (allProductsSuccessfullyArranged)
            {
                return new ArrangingResult {
                    BakingPrograms = programsProductsShouldBeArrangedTo,
                    AllProductsCanBeSuccessfullyArranged = true,
                    IsThereEnoughStockedProducts = thereIsEnoughStock
                };
            }

            List<BakingProgram> newPrograms = _arrangingService.GetNewProgramsProductsShouldBeArrangedInto(orderId);
            programsProductsShouldBeArrangedTo.AddRange(newPrograms);

            allProductsSuccessfullyArranged = !_arrangingService.ThereAreProductsLeftForArranging();
            return new ArrangingResult
            {
                BakingPrograms = programsProductsShouldBeArrangedTo,
                AllProductsCanBeSuccessfullyArranged = allProductsSuccessfullyArranged,
                IsThereEnoughStockedProducts = thereIsEnoughStock
            };

        }

        //TODO get user from request
        public StartPreparingResponse GetDataForPreparing(BakingProgram bakingProgram)
        {
            _startPreparingService.SetProgramToPrepare(bakingProgram);

            if (bakingProgram.Status.Equals(BakingProgramStatus.Created))
            {
                _startPreparingService.UseReservedProductsFromOrdersForPreparing();
                bakingProgram.StartPreparing(new Guid("00000000-0000-0000-0000-000000000001"));
                _bakingProgramRepository.Update(bakingProgram);
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

        public AvailableProgramsResponse FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body, Guid orderId)
        {
            ArrangingResult result = GetExistingOrNewProgramsProductShouldBeArrangedInto(body.ShouldBeDoneAt.Value, body.OrderProducts, orderId);
            AvailableProgramsResponse resultResponse = BakingProgramMapper.CreateAvailableProgramResponse(result);
            return resultResponse;
        }

        public void FinishPreparing(BakingProgram bakingProgram)
        {
            List<ProductToPrepare> productsToPrepare = _productToPrepareRepository.GetByBakingProgramId(bakingProgram.Id);
            foreach (ProductToPrepare productToPrepare in productsToPrepare)
            {
                Guid locationForStockChange = productToPrepare.StockLocationId;

                Guid productForStockChange = productToPrepare.Product.Id;
                int quantityToSubstract = productToPrepare.QuantityToPrepare;
                _stockedProductService.ChangeStockOnLocationForProduct(locationForStockChange, productForStockChange, quantityToSubstract);

            }

            bakingProgram.FinishPreparing();
            _bakingProgramRepository.Update(bakingProgram);

        }

        //TODO test
        public bool CheckIfProgramIsNextForPreparing(BakingProgram bakingProgram)
        {

            DateTime bakingProgrammedAtTime = bakingProgram.BakingProgrammedAt;
            DateTime timeNow = DateTime.Now;
            int numberOfMinutesBeforeProgrammedTimePreparingIsAllowed = 25;
            DateTime earliestTimeToStartPreparing = bakingProgrammedAtTime.AddMinutes(-numberOfMinutesBeforeProgrammedTimePreparingIsAllowed);

            bool isTooEarlyToPrepareProgram = timeNow.CompareTo(earliestTimeToStartPreparing) < 0;

            if (isTooEarlyToPrepareProgram)
                return false;

            List<BakingProgram> bakingProgramsThatShouldBePrepared = _bakingProgramRepository.GetProgramsWithBakingProgrammedAtBetweenDateTimes(timeNow.AddDays(-1), timeNow.AddMinutes(-numberOfMinutesBeforeProgrammedTimePreparingIsAllowed));
            if (bakingProgramsThatShouldBePrepared.Count == 0)
                return false;

            SortBakingProgramsByProgrammedAtTime(bakingProgramsThatShouldBePrepared);

            Guid ovenId = bakingProgram.Oven.Id;
            List<BakingProgram> programsProgrammedForOven = bakingProgramsThatShouldBePrepared.FindAll(bp => bp.Oven.Id == ovenId);
            if (programsProgrammedForOven.Count == 0)
                return false;

            BakingProgram bakingProgramThatShouldBeNextPreparedForOven = programsProgrammedForOven[0];
            return bakingProgramThatShouldBeNextPreparedForOven.Id.Equals(bakingProgram.Id);
        }

        private void SortBakingProgramsByProgrammedAtTime(List<BakingProgram> bakingPrograms)
        {
            bakingPrograms.Sort((bp1, bp2) => bp1.BakingProgrammedAt.CompareTo(bp2.BakingProgrammedAt));
        }

        public void CancellPreparing(BakingProgram bakingProgram)
        {
            bakingProgram.CancellPreparing();
            _bakingProgramRepository.Update(bakingProgram);
        }

        //TODO user from req
        public AllBakingProgramsResponse GetBakingProgramsForUser()
        {
            Guid userId = new Guid("00000000-0000-0000-0000-000000000001");

            DateTime timeNow = DateTime.Now;
            DateTime startTime = timeNow.AddDays(-2);
            DateTime endTime = timeNow.AddHours(4);
            
            List<BakingProgram> bakingPrograms = _bakingProgramRepository.GetProgramsWithBakingProgrammedAtBetweenDateTimes(startTime, endTime);
            List<BakingProgramStatus> statusesToExclude = new List<BakingProgramStatus> { BakingProgramStatus.Cancelled, BakingProgramStatus.Finished };
            ExcludeProgramsWithStatuses(bakingPrograms, statusesToExclude);
            ChangeStatusToDoneIfBakingIsDone(bakingPrograms);
            List<BakingProgram> programsUserIsPreparing = bakingPrograms.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Preparing) && bp.PreparedByUser.Id == userId);

            if (programsUserIsPreparing.Count == 0)
                return BakingProgramMapper.CreateAllBakingProgramsResponse(bakingPrograms, null);

            BakingProgram programUserIsPreparing = programsUserIsPreparing[0];
            AllBakingProgramsResponse response = BakingProgramMapper.CreateAllBakingProgramsResponse(bakingPrograms, programUserIsPreparing);
            StartPreparingResponse preparingInProgressResponse = GetDataForPreparing(programUserIsPreparing);
            response.PreparingInProgress = preparingInProgressResponse;
            return response;

        }

        private void ExcludeProgramsWithStatuses(List<BakingProgram> bakingPrograms, List<BakingProgramStatus> statusesToExclude)
        {
            foreach(BakingProgramStatus status in statusesToExclude)
            {
                bakingPrograms.RemoveAll(bp => bp.Status.Equals(status));
            }
        }

        private void ChangeStatusToDoneIfBakingIsDone(List<BakingProgram> bakingPrograms)
        {
            List<BakingProgram> programsWithBakingStatus = bakingPrograms.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Baking));
            foreach(BakingProgram program in programsWithBakingStatus)
            {  
                if(program.IsBakingDone())
                {
                    program.FinishBaking();
                    _bakingProgramRepository.Update(program);
                }
            }
        }


        public bool CheckIfProgramIsNextForBaking(BakingProgram bakingProgram)
        {

            DateTime bakingProgrammedAtTime = bakingProgram.BakingProgrammedAt;
            DateTime timeNow = DateTime.Now;
            int numberOfMinutesBeforeProgrammedTimeBakingIsAllowed = 5;
            DateTime earliestTimeToStartBaking = bakingProgrammedAtTime.AddMinutes(-numberOfMinutesBeforeProgrammedTimeBakingIsAllowed);

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
            if (preparedProgramsForOven.Count == 0)
                return false;

            BakingProgram bakingProgramThatShouldBeNextBaked = preparedProgramsForOven[0];
            return bakingProgramThatShouldBeNextBaked.Id.Equals(bakingProgram.Id) && OvenIsNotOccupiedByOtherProgram(ovenId);
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

        public void StartBaking(BakingProgram bakingProgram)
        {
            bakingProgram.StartBaking();
            _bakingProgramRepository.Update(bakingProgram);

        }

        public bool CheckIfUserIsAlreadyPreparingAnotherProgram()
        {
            //TODO user from request
            Guid userId = new Guid("00000000-0000-0000-0000-000000000008");

            DateTime timeNow = DateTime.Now;
            DateTime startTime = timeNow.AddDays(-1);
            DateTime endTime = timeNow.AddHours(4);
            List<BakingProgram> bakingPrograms = _bakingProgramRepository.GetProgramsWithBakingProgrammedAtBetweenDateTimes(startTime, endTime);
            List<BakingProgram> programsBeingPreparedByUser = bakingPrograms.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Preparing) && bp.PreparedByUser.Id.Equals(userId));
            return programsBeingPreparedByUser.Count > 0;
        }

        public BakingProgram GetById(Guid bakingProgramId)
        {
            return _bakingProgramRepository.GetById(bakingProgramId);
        }

        public void Finish(BakingProgram bakingProgram)
        {
            bakingProgram.Finish();
            _bakingProgramRepository.Update(bakingProgram);
        }
    }
}
