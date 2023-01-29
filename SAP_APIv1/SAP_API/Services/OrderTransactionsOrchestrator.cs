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

        public void OrchestrateOrderCreation()
        {
            _orderService.CreateOrder(DateTime.Now, null);
            _bakingProgramService.CreateBakingProgram();
            _bakingProgramService.UpdateBakingProgram(null);
        }
    }
}
