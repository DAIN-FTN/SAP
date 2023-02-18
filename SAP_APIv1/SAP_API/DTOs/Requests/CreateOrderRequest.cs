using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.DTOs.Requests
{
    public class CreateOrderRequest
    {
        public DateTime ShouldBeDoneAt { get; set; }
        public Customer Customer { get; set; }
        public List<OrderProductRequest> OrderProducts { get; set; }
    }
}
