using SAP_API.DTOs.Responses.StartPreparing;
using System;
using System.Collections.Generic;

namespace SAP_API.DTOs.Responses
{
    public class LocationToPrepareFromResponse
    {
        public Guid LocationId { get; set; }
        public string LocationCode { get; set; }
        public List<ProductToPrepareResponse> Products { get; set; }
    }
}
