

using System;

namespace SAP_API.DTOs.Responses.StockLocation
{
    public class StockLocationResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
    }
}
