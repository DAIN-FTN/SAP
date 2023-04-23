using System;

namespace SAP_API.DTOs.Responses
{
    public class CreateStockedProductResponse
    {
        public Guid LocationId { get; set; }
        public string LocationCode { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
    }
}
