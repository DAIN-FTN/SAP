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
        public Tuple<bool, List<BakingProgram>> GetExsistingOrNewProgramsProductShouldBeArrangedInto(DateTime timeOrderShouldBeDone, List<OrderProductRequest> orderProducts);
        public List<BakingProgramResponse> FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body);
    }
}
