using SAP_API.DTOs;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses.Order;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public interface IOrderService
    {

        public Order GetById(Guid id);
        public List<OrderResponse> GetOrders();
        public OrderDetailsResponse GetOrderDetails(Order order);
        public Order CreateOrder(CreateOrderInput input, Guid orderId = new Guid());

    }
}
