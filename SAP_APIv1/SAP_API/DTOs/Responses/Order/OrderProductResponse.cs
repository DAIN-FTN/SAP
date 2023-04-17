using System;


namespace SAP_API.DTOs.Responses.Order
{
    public class OrderProductResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantityToBake { get; set; }
    }
}
