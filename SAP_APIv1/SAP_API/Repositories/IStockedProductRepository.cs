using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Repositories
{
    public interface IStockedProductRepository: IRepository<StockedProduct>
    {
        public List<StockedProduct> GetByProductId(Guid productId);
    }
}
