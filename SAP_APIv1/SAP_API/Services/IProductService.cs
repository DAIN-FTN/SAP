using SAP_API.DTOs;
using SAP_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public interface IProductService
    {

        public List<ProductStockResponse> GetProductStock(string name);
    }
}
