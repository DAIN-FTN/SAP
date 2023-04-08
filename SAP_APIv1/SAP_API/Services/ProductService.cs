using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
using SAP_API.Mappers;
using SAP_API.Models;
using SAP_API.Repositories;
using System;
using System.Collections.Generic;

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

        public List<ProductResponse> GetAll(string name)
        {
            List<Product> products;

            if (String.IsNullOrEmpty(name))
            {
                products = (List<Product>)_productRepository.GetAll();
                return ProductMapper.CreateListOfProductResponse(products);
            }

            products = (List<Product>)_productRepository.GetByName(name);
            return ProductMapper.CreateListOfProductResponse(products);


        }

        public Product GetById(Guid productId)
        {
            return _productRepository.GetById(productId);
        }

        public ProductDetailsResponse GetProductDetails(Guid id)
        {
            List<StockedProduct> stockDetails = _stockedProductRepository.GetByProductId(id);
            Product productDetails = _productRepository.GetById(id);

            return ProductMapper.CreateProductDetailsResponse(stockDetails, productDetails);

        }

        public List<ProductStockResponse> GetProductStock(string name)
        {
            List<ProductStockResponse> resultList = new List<ProductStockResponse>();
            List<Product> products = _productRepository.GetByName(name);
            foreach(Product prod in products)
            {
                List<StockedProduct> stockedProducts = _stockedProductRepository.GetByProductId(prod.Id);
                int availableQuantity = 0;
                foreach(StockedProduct stockedProd in stockedProducts)
                {
                    availableQuantity += stockedProd.Quantity - stockedProd.ReservedQuantity;
                }
                ProductStockResponse dto = ProductMapper.CreateProductStockDTO(prod, availableQuantity);
                resultList.Add(dto);
            }

            return resultList;
        }
    }
}
