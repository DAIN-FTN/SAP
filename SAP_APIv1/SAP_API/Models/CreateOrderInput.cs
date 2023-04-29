using SAP_API.DTOs;
using System;
using System.Collections.Generic;

namespace SAP_API.Models
{
    public class CreateOrderInput
    {
        public Customer Customer { get; set; }
        public DateTime ShouldBeDoneAt { get; set; }
        public List<ReservedOrderProduct> Products { get; set; }

    }
}
