using SAP_API.Models;
using SAP_API.Validation;
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

        public CustomValidationResult IsValid()
        {
            if (ShouldBeDoneAtIsInThePast())
            {
                return new CustomValidationResult(false, "Cannot create order for this date, choose a date in the future");
            }
            return new CustomValidationResult(true, "");
        }

        private bool ShouldBeDoneAtIsInThePast()
        {
            return DateTime.Compare((DateTime)ShouldBeDoneAt, DateTime.Now) <= 0;
        }

    }
}
