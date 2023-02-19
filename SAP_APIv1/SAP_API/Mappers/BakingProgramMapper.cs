using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Models;
using SAP_API.Services;
using System;
using System.Collections.Generic;

namespace SAP_API.Mappers
{
    public class BakingProgramMapper
    {
        public static AvailableBakingProgramResponse CreateAvailableBakingProgramResponse(BakingProgram program)
        {
            return new AvailableBakingProgramResponse { 
                Id = program.Id,
                Code = program.Code,
                CreatedAt = program.CreatedAt,
                Status = Enum.GetName(typeof(BakingProgramStatus), program.Status),
                BakingTimeInMins = program.BakingTimeInMins,
                BakingTempInC = program.BakingTempInC,
                BakingProgrammedAt = program.BakingProgrammedAt,
                BakingStartedAt = program.BakingStartedAt,
                OvenCode = program.Oven.Code
            };
        }

        public static AvailableProgramsResponse CreateAvailableProgramResponse(ArrangingResult arragingResult)
        {
            List<BakingProgram> programs = arragingResult.BakingPrograms;
            List<AvailableBakingProgramResponse> resultList = new List<AvailableBakingProgramResponse>();
            foreach(BakingProgram program in programs)
            {
                AvailableBakingProgramResponse result = CreateAvailableBakingProgramResponse(program);
                resultList.Add(result);
            }

            return new AvailableProgramsResponse
            {
                BakingPrograms = resultList,
                AllProductsCanBeSuccessfullyArranged = arragingResult.AllProductsCanBeSuccessfullyArranged,
                IsThereEnoughStockedProducts = arragingResult.IsThereEnoughStockedProducts
            };
        }

        public static AllBakingProgramsResponse CreateAllBakingProgramsResponse(List<BakingProgram> bakingPrograms, BakingProgram programUserIsPreparing)
        {
            List<BakingProgram> programsToPrepare = bakingPrograms.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Created));
            List<BakingProgram> preparedPrograms = bakingPrograms.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Prepared));
            List<BakingProgram> programsBaking = bakingPrograms.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Baking));
            List<BakingProgram> programsDone = bakingPrograms.FindAll(bp => bp.Status.Equals(BakingProgramStatus.Done));


            List<BakingProgramResponse> programsToPrepareResponse = CreateListOfBakingProgramResponses(programsToPrepare);
            List<BakingProgramResponse> preparedProgramsResponse = CreateListOfBakingProgramResponses(preparedPrograms);
            List<BakingProgramResponse> programsBakingResponse = CreateListOfBakingProgramResponses(programsBaking);
            List<BakingProgramResponse> programsDoneResponse = CreateListOfBakingProgramResponses(programsDone);

            AllBakingProgramsResponse response = new AllBakingProgramsResponse
            {
                PrepareForOven = programsToPrepareResponse,
                PreparingAndPrepared = preparedProgramsResponse,
                PreparingInProgress = null,
                Baking = programsBakingResponse,
                Done = programsDoneResponse
            };

            if (programUserIsPreparing == null)
                return response;

            BakingProgramResponse programUserIsPreparingResponse = CreateBakingProgramResponse(programUserIsPreparing);
            response.PreparingAndPrepared.Add(programUserIsPreparingResponse);

            return response;
        }

        private static List<BakingProgramResponse> CreateListOfBakingProgramResponses(List<BakingProgram> programs)
        {
            List<BakingProgramResponse> programsResponse = new List<BakingProgramResponse>();
            foreach (BakingProgram program in programs)
            {
                programsResponse.Add(CreateBakingProgramResponse(program));
            }
            return programsResponse;
        }

        private static BakingProgramResponse CreateBakingProgramResponse(BakingProgram program)
        {

            DateTime canBePreparedAt = program.GetTimeProgramCanBePreparedAt();
            DateTime canBeBakedAt = program.GetTimeProgramCanBeBakedAt();

            return new BakingProgramResponse
            {
                Id = program.Id,
                Code = program.Code,
                CreatedAt = program.CreatedAt,
                Status = Enum.GetName(typeof(BakingProgramStatus), program.Status),
                BakingTimeInMins = program.BakingTimeInMins,
                BakingTempInC = program.BakingTempInC,
                BakingProgrammedAt = program.BakingProgrammedAt,
                CanBePreparedAt = canBePreparedAt,
                CanBeBakedAt = canBeBakedAt,
                BakingStartedAt = program.BakingStartedAt,
                OvenId = program.Oven.Id,
                OvenCode = program.Oven.Code
            };
        }
    }
}
