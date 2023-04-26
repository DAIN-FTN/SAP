using SAP_API.Models;
using System;
using System.Collections.Generic;


namespace SAP_API.DTOs.Responses.Order
{
    public class OrderDetailsResponse
    {
        public Guid Id { get; set; }
        public DateTime ShouldBeDoneAt { get; set; }
        public OrderStatus Status { get; set; }
        public Customer Customer { get; set; }
        public List<OrderProductResponse> Products { get; set; }
        public List<OrderBakingProgramResponse> BakingPrograms { get; set; }
    }
}
