using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs;
using SAP_API.DTOs.Requests;
using SAP_API.DTOs.Responses;
using SAP_API.Mappers;
using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockedProductRepository _stockedProductRepository;
        private readonly IStockedProductService _stockedProductService;

        public ProductService(IProductRepository productRepository, IStockedProductRepository stockedProductRepository, IStockedProductService stockedProductService)
        {
            _productRepository = productRepository;
            _stockedProductRepository = stockedProductRepository;
            _stockedProductService = stockedProductService;

        }

        public CreateProductResponse CreateProduct(CreateProductRequest body)
        {
            Product product = ProductMapper.CreateProductFromCreateProductRequest(body);
            Product created = _productRepository.Create(product);
            List<CreateStockedProductResponse> stockedProducts = CreateStockForProduct(body.Stock, created.Id);

            CreateProductResponse response =  ProductMapper.CreateCreateProductResponseFromProduct(created);
            response.Stock = stockedProducts;
            return response;
        }

        private List<CreateStockedProductResponse> CreateStockForProduct(List<CreateStockedProductRequest> body, Guid productId)
        {
            List<CreateStockedProductResponse> response = new List<CreateStockedProductResponse>();
           
            foreach(CreateStockedProductRequest stockedProduct in body)
            {
                stockedProduct.ProductId = productId;
                CreateStockedProductResponse createdStockedProduct = _stockedProductService.Create(stockedProduct);
                response.Add(createdStockedProduct);
            }

            return response;
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

        public UpdateProductResponse UpdateProduct(Product product, UpdateProductRequest body)
        {
            UpdateProductFields(product, body);
            Product updated = _productRepository.Update(product);
            return ProductMapper.CreateUpdateProductResponseFromProduct(updated);
        }

        private void UpdateProductFields(Product product, UpdateProductRequest body)
        {
            product.BakingTempInC = (int)body.BakingTempInC;
            product.BakingTimeInMins = (int)body.BakingTimeInMins;
            product.Name = body.Name;
            product.Size = (int)body.Size;
        }
    }
}
