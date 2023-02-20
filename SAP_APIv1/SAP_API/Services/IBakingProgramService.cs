using SAP_API.Models;
using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
using System.Collections.Generic;
using System;
using SAP_API.DTOs.Requests;

namespace SAP_API.Services
{
    public interface IBakingProgramService
    {

        public void CreateBakingProgram(BakingProgram bakingProgram);
        public void UpdateBakingProgram(BakingProgram bakingProgram);
        public StartPreparingResponse GetDataForPreparing(BakingProgram bakingProgram);
        public bool CheckIfProgramIsNextForPreparing(BakingProgram bakingProgram);
        public void CancellPreparing(BakingProgram bakingProgram);
        public void FinishPreparing(BakingProgram bakingProgram);
        public AvailableProgramsResponse FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body);
        public ArrangingResult GetExistingOrNewProgramsProductShouldBeArrangedInto(DateTime timeOrderShouldBeDone, List<OrderProductRequest> orderProducts);
        public AllBakingProgramsResponse GetBakingProgramsForUser();
        bool CheckIfProgramIsNextForBaking(BakingProgram bakingProgram);
        void StartBaking(BakingProgram bakingProgram);
        bool CheckIfUserIsAlreadyPreparingAnotherProgram();
        BakingProgram GetById(Guid bakingProgramId);
    }
}

 