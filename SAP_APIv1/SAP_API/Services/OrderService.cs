
﻿using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses.Order;
﻿using SAP_API.DataAccess.Repositories;
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

        public OrderResponse GetOrders()
        {

            return null;
        }

        public OrderDetailsResponse GetOrderDetails()
        {

            return null;
        }

    }
}
