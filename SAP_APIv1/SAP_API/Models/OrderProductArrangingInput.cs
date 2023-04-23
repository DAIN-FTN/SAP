using SAP_API.DTOs;

namespace SAP_API.Models
{
    public class OrderProductArrangingInput: OrderProductRequest
    {
        public int OrderId { get; set; }
    }
}
