﻿using SAP_API.DTOs;
using SAP_API.Mappers;
using SAP_API.Models;
using SAP_API.Repositories;
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