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
        private readonly IBakingProgramRepository _bakingProgramRepository;

        public BakingProgramService(IProductRepository productRepository, IBakingProgramRepository bakingProgramRepository)
        {
            _productRepository = productRepository;
            _bakingProgramRepository = bakingProgramRepository;
        }

        internal class TimeAndTempKey
        {
            public string Key { get; }
            public int Temp { get; set; }
            public int Time { get; set; }


            public TimeAndTempKey(int temp, int time)
            {
                Key = temp + "_" + time;
                Temp = temp;
                Time = time;

            }

            public override bool Equals(object obj)
            {
                if (obj is TimeAndTempKey)
                    return ((TimeAndTempKey)obj).Key.Equals(this.Key);
                return false;
            }

            public override int GetHashCode()
            {
                return this.Key.GetHashCode();
            }

        }

        internal class ProductQuantity
        {
            public Product Product {get; set;}
            public int Quantity { get; set; }


        }

        public List<BakingProgramResponse> FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body)
        {
            Dictionary<TimeAndTempKey, List<ProductQuantity>> productsDict = new Dictionary<TimeAndTempKey, List<ProductQuantity>>();
            List<BakingProgramResponse> resultList = new List<BakingProgramResponse>();
            List<OrderProductRequest> orderProducts = body.OrderProducts;

            populateProductsWithSameTimeAndTempDictionary(productsDict, orderProducts);

            IEnumerable<TimeAndTempKey> keys = productsDict.Keys;
            foreach(TimeAndTempKey key in keys)
            {
                int temp = key.Temp;
                int time = key.Time;
                List<ProductQuantity> products = productsDict[key];
                List<BakingProgram> bakingPrograms = _bakingProgramRepository.GetByTempAndTime(temp, time);
                int bakingProgramIndex = 0;
                int bakingProgramsCount = bakingPrograms.Count;
                int productIndex = 0;
                int productCount = products.Count;

                while(bakingProgramIndex < bakingProgramsCount && productIndex < productCount)
                {
                    BakingProgram program = bakingPrograms[bakingProgramIndex];
                    int remainingCapacity = program.RemainingOvenCapacity;
                    while(remainingCapacity > 0 && productIndex < productCount)
                    {
                        ProductQuantity product = products[productIndex];
                        int size = product.Product.Size;
                        int quantity = product.Quantity;
                        int numberOfProductsFittingInOven = remainingCapacity / size;
                        int numberOfProductsForOven = Math.Min(quantity, numberOfProductsFittingInOven);
                        if(numberOfProductsForOven == 0)
                        {
                            bakingProgramIndex++;
                            break;
                        }
                        remainingCapacity -= size * numberOfProductsForOven;
                        product.Quantity -= numberOfProductsForOven;
                        if(product.Quantity == 0)
                        {
                            productIndex++;
                        }

                    }
                }

                if(productIndex < productCount)
                {

                }
                


            }



            return resultList;
        }

        private void populateProductsWithSameTimeAndTempDictionary(Dictionary<TimeAndTempKey, List<ProductQuantity>> productsDict, List<OrderProductRequest> products)
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
                    productsDict[key].Add(new ProductQuantity 
                    { 
                        Product = productDetails,
                        Quantity = quantity
                    });
                else
                {
                    List<ProductQuantity> productsWithSameBakingTimeAndTemp = new List<ProductQuantity>();
                    productsWithSameBakingTimeAndTemp.Add(new ProductQuantity
                    {
                        Product = productDetails,
                        Quantity = quantity
                    });
                    productsDict.Add(key, productsWithSameBakingTimeAndTemp);
                }


            }
        }
    }
}
