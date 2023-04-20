using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DataAccess.Repositories
{
    public interface IBakingProgramProductRepository: IRepository<BakingProgramProduct>
    {
        public List<BakingProgramProduct> GetByOrderId(Guid orderId);
    }
}
