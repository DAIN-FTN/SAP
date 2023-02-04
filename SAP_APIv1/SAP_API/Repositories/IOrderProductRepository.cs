using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Repositories
{
    public interface IOrderProductRepository
    {
        public List<OrderProduct> GetByOrderIdAndProductId(Guid orderId, Guid productId);
    }
}
