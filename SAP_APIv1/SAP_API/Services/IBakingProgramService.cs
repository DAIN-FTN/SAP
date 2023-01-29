using SAP_API.Models;

namespace SAP_API.Services
{
    public interface IBakingProgramService
    {
        public void CreateBakingProgram();
        public void UpdateBakingProgram(BakingProgram bakingProgram);
    }
}
