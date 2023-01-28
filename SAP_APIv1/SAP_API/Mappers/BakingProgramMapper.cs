using SAP_API.DTOs.Responses;
using SAP_API.Models;

namespace SAP_API.Mappers
{
    public class BakingProgramMapper
    {
        public BakingProgramResponse CreateBakingProgramsResponse(BakingProgram program)
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
    }
}
