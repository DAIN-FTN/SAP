using SAP_API.DTOs;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public interface IArrangingProductsToProgramsService

    {
        public void SetTimeOrderShouldBeDone(DateTime timeOrderShouldBeDone);

        public void PrepareProductsForArranging(List<OrderProductRequest> orderProducts);

        public List<BakingProgram> GetExistingProgramsProductShouldBeArrangedInto(Guid orderId);
        public List<BakingProgram> GetNewProgramsProductsShouldBeArrangedInto(Guid orderId);
        public bool ThereAreProductsLeftForArranging();
    }
}
