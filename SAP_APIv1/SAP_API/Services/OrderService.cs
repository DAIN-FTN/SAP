
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

        public Order CreateOrder(CreateOrderRequest createOrderRequest)
        {
            List<ReservedOrderProduct> orderProducts = new List<ReservedOrderProduct>();

            foreach (var productRequest in createOrderRequest.Products)
            {
                Product product = _productRepository.GetById(productRequest.ProductId.Value);
                orderProducts.Add(new ReservedOrderProduct
                {
                    Product = product,
                    Id = Guid.NewGuid(),
                    ReservedQuantity = productRequest.Quantity.Value,
                });
            }

            Order order = new Order
            {
                CustomerFullName = createOrderRequest.Customer.FullName,
                CustomerEmail = createOrderRequest.Customer.Email,
                CustomerTelephone = createOrderRequest.Customer.Telephone,
                ShouldBeDoneAt = createOrderRequest.ShouldBeDoneAt,
                Status = OrderStatus.Created,
                Products = orderProducts,
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
