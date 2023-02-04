using SAP_API.DTOs.Responses.StartPreparing;
using System;
using System.Collections.Generic;

namespace SAP_API.DTOs.Responses
{
    public class StartPreparingFromLocationResponse
    {
        public Guid LocationId { get; set; }
        public string LocationCode { get; set; }
        public List<StartPreparingProductFromOrderResponse> Products { get; set; }
    }
}
