using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DTOs.Responses
{
    public class AllBakingProgramsResponse
    {
        public List<BakingProgramResponse> PrepareForOven { get; set; }
        public List<BakingProgramResponse> PreparingAndPrepared {get; set;}
        public StartPreparingResponse PreparingInProgress { get; set; }
        public List<BakingProgramResponse> Baking { get; set; }
        public List<BakingProgramResponse> Done { get; set; }
    }
}
