using SAP_API.DTOs.Requests;
using SAP_API.Models;
using System;

namespace SAP_API.Mappers
{
    public class OrderMapper
    {
        public static Order CreateOrderRequestToOrder(CreateOrderRequest createOrderRequest)
        {
            return new Order
            {
                Customer = createOrderRequest.Customer,
                Id = Guid.NewGuid(),
                Products = createOrderRequest.Products,
                ShouldBeDoneAt = createOrderRequest.ShouldBeDoneAt,
                Status = OrderStatus.Created
            };
        }
    }
}
