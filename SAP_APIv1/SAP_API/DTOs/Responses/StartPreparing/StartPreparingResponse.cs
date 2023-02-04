using System;
using System.Collections.Generic;

namespace SAP_API.DTOs.Responses
{
    public class StartPreparingResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int BakingTimeInMins { get; set; }
        public int BakingTempInC { get; set; }
        public DateTime BakingProgrammedAt { get; set; }
        public Guid OvenId { get; set; }
        public string OvenCode { get; set; }
        public List<StartPreparingFromLocationResponse> Locations { get; set; }

    }
}
