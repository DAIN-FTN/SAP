using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DTOs
{
    public class FindAvailableBakingProgramsRequest
    {
        public DateTime ShouldBeDoneAt { get; set; }
        public List<OrderProductRequest> OrderProducts { get; set; }

    }
}
