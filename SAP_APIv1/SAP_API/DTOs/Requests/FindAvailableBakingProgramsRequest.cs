using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DTOs
{
    public class FindAvailableBakingProgramsRequest
    {
        [Required] public DateTime? ShouldBeDoneAt{ get; set; }
        [Required] public List<OrderProductRequest>? OrderProducts { get; set; }

    }
}
