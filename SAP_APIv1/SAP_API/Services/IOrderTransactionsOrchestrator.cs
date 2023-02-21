using Microsoft.AspNetCore.Mvc;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;

namespace SAP_API.Services
{
    public interface IOrderTransactionsOrchestrator
    {
        public CreateOrderResponse OrchestrateOrderCreation(CreateOrderRequest orderCreationRequest);
    }
}
