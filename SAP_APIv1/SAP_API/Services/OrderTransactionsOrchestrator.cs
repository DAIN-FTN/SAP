using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Exceptions;
using SAP_API.Mappers;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace SAP_API.Services
{
    public class OrderTransactionsOrchestrator: IOrderTransactionsOrchestrator
    {
        private readonly IOrderService _orderService;
        private readonly IBakingProgramService _bakingProgramService;
        private readonly IStockedProductService _stockedProductService;

        public OrderTransactionsOrchestrator(
            IOrderService orderService,
            IBakingProgramService bakingProgramService,
            IStockedProductService stockedProductService
            )
        {
            _orderService = orderService;
            _bakingProgramService = bakingProgramService;
            _stockedProductService = stockedProductService;
        }

        public CreateOrderResponse OrchestrateOrderCreation(CreateOrderRequest orderCreationRequest)
        {
            using (var scope = new TransactionScope())
            try
            {
                    Guid orderId = new Guid();

                    List<ReservedOrderProduct> reservedOrderProducts = _stockedProductService.reserveStockedProducts(orderCreationRequest.Products, orderId);

                    Order order = _orderService.CreateOrder(
                        new CreateOrderInput
                        {
                            Customer = orderCreationRequest.Customer,
                            ShouldBeDoneAt = orderCreationRequest.ShouldBeDoneAt,
                            Products = reservedOrderProducts
                        },
                        orderId
                    );

                    ArrangingResult arrangingResult = _bakingProgramService.GetExistingOrNewProgramsProductShouldBeArrangedInto(orderCreationRequest.ShouldBeDoneAt, orderCreationRequest.Products, order.Id);

                    if (arrangingResult.AllProductsCanBeSuccessfullyArranged == false)
                    {
                        throw new OrderCreationException("Products cannot be succesfully arranged");
                    }

                    if (arrangingResult.IsThereEnoughStockedProducts == false)
                    {
                        throw new OrderCreationException("There isn't enough products in stock");
                    }


                    foreach (var bakingProgram in arrangingResult.BakingPrograms)
                {
                        if (bakingProgram.Status == BakingProgramStatus.Pending)
                    {
                        _bakingProgramService.CreateBakingProgram(bakingProgram);
                    }
                    else
                    {
                        _bakingProgramService.UpdateBakingProgram(bakingProgram);
                    }
                }

                scope.Complete();

                return OrderMapper.OrderToCreateOrderResponse(order);
            }
            catch(NotEnoughStockedProductException exception)
            {
                throw new OrderCreationException(exception.Message);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
