using SAP_API.DTOs;
using SAP_API.Mappers;
using SAP_API.Models;
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
        private readonly IStockedProductRepository _stockedProductRepository;

        public ProductService(IProductRepository productRepository, IStockedProductRepository stockedProductRepository)
        {
            _productRepository = productRepository;
            _stockedProductRepository = stockedProductRepository;

        }

        public List<ProductStockDTO> GetProductStock(string name)
        {
            List<ProductStockDTO> resultList = new List<ProductStockDTO>();
            List<Product> products = _productRepository.GetByName(name);
            foreach(Product prod in products)
            {
                List<StockedProduct> stockedProducts = _stockedProductRepository.GetByProductId(prod.Id);
                int availableQuantity = 0;
                foreach(StockedProduct stockedProd in stockedProducts)
                {
                    availableQuantity += stockedProd.Quantity - stockedProd.ReservedQuantity;
                }
                ProductStockDTO dto = ProductMapper.CreateProductStockDTO(prod, availableQuantity);
                resultList.Add(dto);
            }

            return resultList;
        }

        public void UpdateProductStock(ProductStockDTO productStock)
        {
            throw new NotImplementedException();
        }
    }
}
