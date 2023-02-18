using SAP_API.Models;
using SAP_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order CreateOrder(DateTime shouldBeDoneAt, Customer customer, List<OrderProduct> orderProducts)
        {
            Order order = new Order() 
            {
                   Customer = customer,
                   Id= Guid.NewGuid(),
                   ShouldBeDoneAt = shouldBeDoneAt,
                   Status = OrderStatus.Created,
                   Products = orderProducts,
                   
            };
            
            return _orderRepository.Create(order);
        }
    }
}
