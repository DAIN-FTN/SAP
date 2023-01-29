using SAP_API.Models;
using System.Collections.Generic;

namespace SAP_API.Repositories
{
    public interface IBakingProgramRepository: IRepository<BakingProgram>
    {
        public List<BakingProgram> GetByTempAndTime(int temp, int time);
    }
}