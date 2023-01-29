using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
using System.Collections.Generic;
using SAP_API.Models;

namespace SAP_API.Services
{
    public interface IBakingProgramService
    {
        public List<BakingProgramResponse> FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body);
        public void CreateBakingProgram();
        public void UpdateBakingProgram(BakingProgram bakingProgram);
    }
}