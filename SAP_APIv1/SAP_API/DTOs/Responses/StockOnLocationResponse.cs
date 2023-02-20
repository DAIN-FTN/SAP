using System;

namespace SAP_API.DTOs.Responses
{
    public class StockOnLocationResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }

    }
}