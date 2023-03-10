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
            ArrangingResult arrangingResult = _bakingProgramService.GetExistingOrNewProgramsProductShouldBeArrangedInto(orderCreationRequest.ShouldBeDoneAt, orderCreationRequest.Products);

            if(arrangingResult.AllProductsCanBeSuccessfullyArranged == false)
            {
                throw new OrderCreationException("Products cannot be succesfully arranged");
            }

            if(arrangingResult.IsThereEnoughStockedProducts == false)
            {
                throw new OrderCreationException("There isn't enough products in stock");
            }

            using (var scope = new TransactionScope())
            try
            {
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

                _stockedProductService.reserveStockedProducts(orderCreationRequest.Products);

                Order order = _orderService.CreateOrder(orderCreationRequest);

                scope.Complete();

                return OrderMapper.OrderToOrderResponse(order);
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
