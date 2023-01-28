using SAP_API.Models;
using System;

namespace SAP_API.DTOs.Responses
{
    public class BakingProgramResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public BakingPogramStatus Status { get; set; }
        public int BakingTimeInMins { get; set; }
        public int BakingTempInC { get; set; }
        public DateTime BakingProgrammedAt { get; set; }
        public DateTime BakingStartedAt { get; set; }
        public string OvenCode { get; set; }

    }
}
