using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DataAccess.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public List<Product> GetByName(string name);
    }
}
