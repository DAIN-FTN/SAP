using System;
using System.Collections.Generic;

namespace SAP_API.DTOs.Responses
{
    public class ProductDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BakingTimeInMins { get; set; }
        public int BakingTempInC { get; set; }
        public int Size { get; set; }

        public List<StockOnLocationResponse> LocationsWithStock { get; set; }
    }
}
