using SAP_API.DTOs.Requests;
using System;

namespace SAP_API.Services
{
    public class OrderTransactionsOrchestrator: IOrderCreationOrchestrator
    {
        private readonly IOrderService _orderService;
        private readonly IBakingProgramService _bakingProgramService;
        private readonly IProductService _productService;

        public OrderTransactionsOrchestrator(
            IOrderService orderService,
            IBakingProgramService bakingProgramService,
            IProductService productService
            )
        {
            _orderService = orderService;
            _bakingProgramService = bakingProgramService;
            _productService = productService;
        }

        //TODO: implement rollback
        public void OrchestrateOrderCreation(CreateOrderRequest orderCreationRequest)
        {
            _orderService.CreateOrder(orderCreationRequest.ShouldBeDoneAt, orderCreationRequest.Customer, orderCreationRequest.OrderProducts);



            _bakingProgramService.ArrangeProductsIntoNewOrExistingPrograms();

        }
    }
}
