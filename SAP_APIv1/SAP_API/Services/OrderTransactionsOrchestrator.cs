using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Mappers;
using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.Services
{
    public class OrderTransactionsOrchestrator: IOrderCreationOrchestrator
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

        //TODO: implement rollback
        public CreateOrderResponse OrchestrateOrderCreation(CreateOrderRequest orderCreationRequest)
        {
            ArrangingResult arrangingResult = _bakingProgramService.GetExistingOrNewProgramsProductShouldBeArrangedInto(orderCreationRequest.ShouldBeDoneAt, orderCreationRequest.Products);

            if(arrangingResult.AllProductsCanBeSuccessfullyArranged == false)
            {
                throw new Exception();
            }

            if(arrangingResult.IsThereEnoughStockedProducts == false)
            {
                throw new Exception();
            }

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

                return OrderMapper.OrderToOrderResponse(order);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
