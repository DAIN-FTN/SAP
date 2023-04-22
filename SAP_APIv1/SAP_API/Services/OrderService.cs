using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs.Requests;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SAP_API.Services.ArrangingProductsToProgramsService;

namespace SAP_API.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;
        private readonly  IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public Order CreateOrder(CreateOrderInput createOrderInput, Guid orderId = new Guid())
        {
            Order order = new Order
            {
                Id = orderId,
                ShouldBeDoneAt = createOrderInput.ShouldBeDoneAt,
                CustomerFullName = createOrderInput.Customer.FullName,
                CustomerEmail = createOrderInput.Customer.Email,
                CustomerTelephone = createOrderInput.Customer.Telephone,
                Status = OrderStatus.Created,
                Products = createOrderInput.Products,
            };
            
            return _orderRepository.Create(order);
        }

    }
}
