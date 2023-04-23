using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.DataAccess.Repositories
{
    public interface IBakingProgramRepository : IRepository<BakingProgram>
    {
        public List<BakingProgram> GetByTempAndTime(int temp, int time);
        public List<BakingProgram> GetByOvenId(Guid ovenId);
        public List<BakingProgram> GetProgramsWithBakingProgrammedAtBetweenDateTimes(DateTime startTime, DateTime endTime);
    }
}