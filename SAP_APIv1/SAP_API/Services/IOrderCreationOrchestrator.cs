using SAP_API.DTOs.Requests;

namespace SAP_API.Services
{
    public interface IOrderCreationOrchestrator
    {
        public void OrchestrateOrderCreation(CreateOrderRequest orderCreationRequest);
    }
}
