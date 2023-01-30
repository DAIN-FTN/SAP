using SAP_API.DTOs.Responses;
using SAP_API.Models;
using System.Collections.Generic;

namespace SAP_API.Mappers
{
    public class BakingProgramMapper
    {
        public static BakingProgramResponse CreateBakingProgramResponse(BakingProgram program)
        {
            return new BakingProgramResponse { 
                Id = program.Id,
                Code = program.Code,
                CreatedAt = program.CreatedAt,
                Status = program.Status,
                BakingTimeInMins = program.BakingTimeInMins,
                BakingTempInC = program.BakingTempInC,
                BakingProgrammedAt = program.BakingProgrammedAt,
                BakingStartedAt = program.BakingStartedAt,
                OvenCode = program.Oven.Code
            };
        }

        public static List<BakingProgramResponse> CreateListOfBakingProgramResponse(List<BakingProgram> programs)
        {
            List<BakingProgramResponse> resultList = new List<BakingProgramResponse>();
            foreach(BakingProgram program in programs)
            {
                BakingProgramResponse result = CreateBakingProgramResponse(program);
                resultList.Add(result);
            }
            return resultList;
        }
    }
}
