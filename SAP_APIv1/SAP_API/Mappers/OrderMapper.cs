using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Models;
using System;

namespace SAP_API.Mappers
{
    public class OrderMapper
    {
        public static CreateOrderResponse OrderToOrderResponse(Order order)
        {
            return new CreateOrderResponse
            {
                Id= order.Id,
                Status  = order.Status, 
            };
        }
    }
}
