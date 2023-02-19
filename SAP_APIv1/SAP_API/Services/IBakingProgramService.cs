using SAP_API.Models;
using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
using System.Collections.Generic;
using System;

namespace SAP_API.Services
{
    public interface IBakingProgramService
    {

        public void CreateBakingProgram();
        public void UpdateBakingProgram(BakingProgram bakingProgram);
        public StartPreparingResponse GetDataForPreparing(Guid id);
        public bool CheckIfProgramIsNextForPreparing(Guid bakingProgramId);
        public void CancellPreparing(Guid bakingProgramId);
        public void FinishPreparing(Guid id);
        public List<AvailableBakingProgramResponse> FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body);
        public ArrangingResult GetExistingOrNewProgramsProductShouldBeArrangedInto(DateTime timeOrderShouldBeDone, List<OrderProductRequest> orderProducts);
        public AllBakingProgramsResponse GetBakingProgramsForUser();
        bool CheckIfProgramIsNextForBaking(Guid bakingProgramId);
        void StartBaking(Guid bakingProgramId);
        bool CheckIfUserIsAlreadyPreparingAnotherProgram();
    }
}

 