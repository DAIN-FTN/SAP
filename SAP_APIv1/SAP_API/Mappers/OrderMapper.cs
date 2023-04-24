using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.DTOs.Responses.Order;
using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.Mappers
{
    public class OrderMapper
    {
        public static CreateOrderResponse OrderToCreateOrderResponse(Order order)
        {
            return new CreateOrderResponse
            {
                Id= order.Id,
            };
        }

        public static OrderResponse OrderToOrderResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                ShouldBeDoneAt = order.ShouldBeDoneAt,
                Status = order.Status,
                Customer = new Customer
                {
                    Email = order.CustomerEmail,
                    FullName = order.CustomerFullName,
                    Telephone = order.CustomerTelephone
                }
            };
        }

        public static List<OrderResponse> OrderListToOrderResponseList(List<Order> orders)
        {
            List<OrderResponse> ordersResponse = new List<OrderResponse>();

            foreach (Order o in orders)
            {
                ordersResponse.Add(OrderToOrderResponse(o));
            }

            return ordersResponse;
        }

        public static OrderDetailsResponse OrderToOrderDetailsResponse(Order order, List<BakingProgramProduct> productsInBakingProgramsFromOrder)
        {
            return new OrderDetailsResponse
            {
                Id = order.Id,
                Customer = new Customer
                {
                    Email = order.CustomerEmail,
                    FullName = order.CustomerFullName,
                    Telephone = order.CustomerTelephone
                },
                ShouldBeDoneAt = order.ShouldBeDoneAt,
                Products = ReservedOrderProductListToOrderProductResponseList(order.Products),
                BakingPrograms = BakingProgramProductListToOrderBakingProgramResponseList(productsInBakingProgramsFromOrder)
            };
        }

        private static List<OrderProductResponse> ReservedOrderProductListToOrderProductResponseList(List<ReservedOrderProduct> reservedProducts)
        {
            List<OrderProductResponse> orderedProducts = new List<OrderProductResponse>();
            Dictionary<Guid, OrderProductResponse> productQuantityToBakeDict = GetProductQuantityToBakeDictionary(reservedProducts);

            foreach (KeyValuePair<Guid, OrderProductResponse> entry in productQuantityToBakeDict)
            {
                orderedProducts.Add(entry.Value);
            }

            return orderedProducts;

        }

        private static Dictionary<Guid, OrderProductResponse> GetProductQuantityToBakeDictionary(List<ReservedOrderProduct> reservedProducts)
        {
            Dictionary<Guid, OrderProductResponse> productOrderQuantityDict = new Dictionary<Guid, OrderProductResponse>();

            foreach (ReservedOrderProduct reservedProduct in reservedProducts)
            {
                Guid productId = reservedProduct.ProductId;
                if (!productOrderQuantityDict.ContainsKey(productId))
                    productOrderQuantityDict.Add(productId, new OrderProductResponse
                    {
                        ProductId = productId,
                        ProductName = reservedProduct.Product.Name,
                        QuantityToBake = 0
                    });

                productOrderQuantityDict[productId].QuantityToBake += reservedProduct.ReservedQuantity;
            }

            return productOrderQuantityDict;
        }

        private static List<OrderBakingProgramResponse> BakingProgramProductListToOrderBakingProgramResponseList(List<BakingProgramProduct> productsInBakingProgramsFromOrder)
        {
            List<OrderBakingProgramResponse> orderBakingPrograms = new List<OrderBakingProgramResponse>();
            Dictionary<Guid, OrderBakingProgramResponse> programProductsToBakeDict = GetProgramProductsToBakeDictionary(productsInBakingProgramsFromOrder);

            foreach (KeyValuePair<Guid, OrderBakingProgramResponse> entry in programProductsToBakeDict)
            {
                orderBakingPrograms.Add(entry.Value);
            }

            return orderBakingPrograms;
        }

        private static Dictionary<Guid, OrderBakingProgramResponse> GetProgramProductsToBakeDictionary(List<BakingProgramProduct> productsInBakingProgramsFromOrder)
        {
            Dictionary<Guid, OrderBakingProgramResponse> programProductsToBakeDictionary = new Dictionary<Guid, OrderBakingProgramResponse>();
            foreach(BakingProgramProduct productForBaking in productsInBakingProgramsFromOrder)
            {
                Guid programId = productForBaking.BakingProgramId;
                BakingProgram program = productForBaking.BakingProgram;
                if (!programProductsToBakeDictionary.ContainsKey(programId))
                    programProductsToBakeDictionary.Add(programId, new OrderBakingProgramResponse
                    {
                        BakingProgrammedAt = program.BakingProgrammedAt,
                        Code = program.Code,
                        BakingTimeInMins = program.BakingTimeInMins,
                        BakingStartedAt = program.BakingStartedAt,
                        OvenCode = program.Oven.Code,
                        Status = program.Status,
                        Products = new List<OrderProductResponse>()
                    });

                programProductsToBakeDictionary[programId].Products.Add(new OrderProductResponse
                {
                    ProductId = productForBaking.ProductId,
                    ProductName = productForBaking.Product.Name,
                    QuantityToBake = productForBaking.QuantityТoBake
                });

            }

            return programProductsToBakeDictionary;
        }

    }
}
