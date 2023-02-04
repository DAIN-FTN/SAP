using SAP_API.DTOs.Responses;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public interface IStartPreparingService
    {
        public List<ReservedOrderProduct> GetInfoAboutReservedProductFromOrder(Guid orderId, Guid productId);

        public StartPreparingResponse CreateStartPreparingResponse(BakingProgram bakingProgram);
        public void GroupProductsToBePreparedByLocation(List<ReservedOrderProduct> reservedProductQuantitiesOnLocations, int quantityToBePrepared);
    }
}
