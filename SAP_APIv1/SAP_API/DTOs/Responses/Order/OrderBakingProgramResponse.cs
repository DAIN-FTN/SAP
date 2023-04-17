using System;
using System.Collections.Generic;


namespace SAP_API.DTOs.Responses.Order
{
    public class OrderBakingProgramResponse
    {
        public string Code { get; set; }
        public DateTime BakingProgrammedAt { get; set; }
        public string Status { get; set; }
        public DateTime? BakingStartedAt { get; set; }
        public int BakingTimeInMins { get; set; }
        public string OvenCode { get; set; }
        public List<OrderProductResponse> Products { get; set; }
    }
}
