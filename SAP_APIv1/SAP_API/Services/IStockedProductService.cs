using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public interface IStockedProductService
    {
        public void ChangeStockOnLocationForProduct(Guid locationId, Guid productId, int quantityToSubstract);
        public void reserveStockedProduct(Guid productId, int quantityToReserve);

    }
}
