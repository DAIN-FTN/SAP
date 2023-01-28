using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
using SAP_API.Models;
using SAP_API.Repositories;
using System;
using System.Collections.Generic;

namespace SAP_API.Services
{
    public class BakingProgramService : IBakingProgramService
    {
        private readonly IProductRepository _productRepository;

        internal class TimeAndTempKey
        {
            string key;
            public TimeAndTempKey(int temp, int time)
            {
                key = temp + "" + time;
            }
        }

        public List<BakingProgramResponse> FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body)
        {
            Dictionary<TimeAndTempKey, List<Product>> productsDict = new Dictionary<TimeAndTempKey, List<Product>>();
            List<BakingProgramResponse> resultList = new List<BakingProgramResponse>();
            List<OrderProductRequest> products = body.OrderProducts;

            populateProductsWithSameTimeAndTempDictionary(productsDict, products);
            

            return resultList;
        }

        private void populateProductsWithSameTimeAndTempDictionary(Dictionary<TimeAndTempKey, List<Product>> productsDict, List<OrderProductRequest> products)
        {
            foreach (OrderProductRequest product in products)
            {
                Guid productId = product.ProductId;
                int quantity = product.Quantity;
                Product productDetails = _productRepository.GetById(productId);
                int temp = productDetails.BakingTempInC;
                int time = productDetails.BakingTimeInMins;
                TimeAndTempKey key = new TimeAndTempKey(temp, time);

                if (productsDict.ContainsKey(key))
                    productsDict[key].Add(productDetails);
                else
                {
                    List<Product> productsWithSameBakingTimeAndTemp = new List<Product>();
                    productsWithSameBakingTimeAndTemp.Add(productDetails);
                    productsDict.Add(key, productsWithSameBakingTimeAndTemp);
                }


            }
        }
    }
}
