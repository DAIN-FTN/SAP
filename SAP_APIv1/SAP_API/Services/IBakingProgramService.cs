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
        public List<BakingProgramResponse> FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body);
        public ArrangingResult GetExistingOrNewProgramsProductShouldBeArrangedInto(DateTime timeOrderShouldBeDone, List<OrderProductRequest> orderProducts);
    }
}

 