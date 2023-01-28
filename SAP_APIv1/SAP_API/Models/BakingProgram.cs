using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Models
{
    public class BakingProgram: IEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public BakingPogramStatus Status { get; set; }
        public int BakingTimeInMins { get; set; }
        public int BakingTempInC { get; set; }
        public DateTime BakingProgrammedAt { get; set; }
        public DateTime BakingStartedAt { get; set; }
        public Oven Oven { get; set; }
        public User PreparedBy { get; set; }
        public int RemainingOvenCapacity { get; set; }
    }
}
