

using SAP_API.Models;
using System;

namespace SAP_API.DTOs.Responses.User
{
    public class PreparedBakingProgramResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public BakingProgramStatus Status { get; set; }
        public int BakingTimeInMins { get; set; }
        public int BakingTempInC { get; set; }
        public DateTime BakingProgrammedAt { get; set; }
        public DateTime? BakingStartedAt { get; set; }
        public Guid OvenId { get; set; }
        public string OvenCode { get; set; }
    }
}
