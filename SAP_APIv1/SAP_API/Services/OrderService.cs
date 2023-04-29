
﻿using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses.Order;
﻿using SAP_API.DataAccess.Repositories;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SAP_API.Services.ArrangingProductsToProgramsService;
using SAP_API.Mappers;

namespace SAP_API.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;
        private readonly  IProductRepository _productRepository;
        private readonly IBakingProgramProductRepository _bakingProgramProductRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IBakingProgramProductRepository bakingProgramProductRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _bakingProgramProductRepository = bakingProgramProductRepository;
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

        public List<OrderResponse> GetOrders()
        {
            List<Order> orders = (List<Order>)_orderRepository.GetAll();
            return OrderMapper.OrderListToOrderResponseList(orders);
        }

        public OrderDetailsResponse GetOrderDetails(Order order)
        {
            List<BakingProgramProduct> productsInBakingProgramsFromOrder = _bakingProgramProductRepository.GetByOrderId(order.Id);
            return OrderMapper.OrderToOrderDetailsResponse(order, productsInBakingProgramsFromOrder);
        }

        public Order GetById(Guid id)
        {
            return _orderRepository.GetById(id);
        }

    }
}
