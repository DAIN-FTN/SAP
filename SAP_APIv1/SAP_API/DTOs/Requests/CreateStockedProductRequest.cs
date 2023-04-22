using System;


namespace SAP_API.DTOs.Requests
{
    public class CreateStockedProductRequest
    {
        public Guid ProductId { get; set; }
        public Guid LocationId { get; set; }
        public int Quantity { get; set; }
    }
}
