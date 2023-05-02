using System;

namespace SAP_API.DTOs
{
    public class ProductStockResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
