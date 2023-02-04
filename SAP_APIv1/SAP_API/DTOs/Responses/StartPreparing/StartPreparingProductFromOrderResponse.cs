using System;


namespace SAP_API.DTOs.Responses.StartPreparing
{
    public class StartPreparingProductFromOrderResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
       public Guid OrderId { get; set; }
    }
}
