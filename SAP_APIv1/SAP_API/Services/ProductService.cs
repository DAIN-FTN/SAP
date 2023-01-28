using SAP_API.DTOs;
using SAP_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public List<ProductStockDTO> GetProductStock(string name)
        {
            throw new NotImplementedException();
        }
    }
}
