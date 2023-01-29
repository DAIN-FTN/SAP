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
        private readonly IOvenRepository _ovenRepository;

        public BakingProgramService(IProductRepository productRepository, IBakingProgramRepository bakingProgramRepository, IOvenRepository ovenRepository)
        {
            _productRepository = productRepository;
            _bakingProgramRepository = bakingProgramRepository;
            _ovenRepository = ovenRepository;
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
            DateTime productsShouldBeDoneAtTime = body.ShouldBeDoneAt;

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
                   bool thereAreFreeTimeSlotsForNewPrograms = checkForFreeTimeSlotsForRemainingProducts(products, key, productsShouldBeDoneAtTime);
                    if (!thereAreFreeTimeSlotsForNewPrograms)
                    {
                        resultList.Clear();
                        return resultList;
                    }
                }
                

            }



            return resultList;
        }

        private bool checkForFreeTimeSlotsForRemainingProducts(List<ProductQuantity> products, TimeAndTempKey key, DateTime productsShouldBeDoneAtTime)
        {
            List<Oven> ovens = (List<Oven>)_ovenRepository.GetAll();
            int numberOfMinsoffsetForBakingTime = 5;
            int bakingTimeInMins = key.Time + numberOfMinsoffsetForBakingTime;
            foreach(Oven o in ovens)
            {
                List<BakingProgram> bakingPrograms = _bakingProgramRepository.GetByOvenId(o.Id);
                int numberOfSlots = findNumberOfAvailableTimeSlotsForOven(bakingPrograms, bakingTimeInMins, productsShouldBeDoneAtTime);

            }
            return false;
        }

        private int findNumberOfAvailableTimeSlotsForOven(List<BakingProgram> bakingPrograms, int bakingTimeInMins, DateTime productShouldBeDoneAt)
        {
            DateTime startTimeForSlots = productShouldBeDoneAt.AddHours(-8);
            DateTime endTimeForSlots = productShouldBeDoneAt.AddHours(-1);
            int numberOfTimeSlots = 0;

            int bakingProgramIndex = 0;
            int bakingProgramsCount = bakingPrograms.Count;

            if(bakingProgramsCount == 0)
            {
                int numberOfMinutesInAvailableTimeSlots = 7 * 3600;
                numberOfTimeSlots = numberOfMinutesInAvailableTimeSlots / bakingTimeInMins;
                return numberOfTimeSlots;
            }

            BakingProgram firtsBakingProgram = bakingPrograms[0];
            DateTime FirstBpProgrammedAtTime = firtsBakingProgram.BakingProgrammedAt;
            int numberOfMinutesBetweenTakenSlots = (int)FirstBpProgrammedAtTime.Subtract(startTimeForSlots).TotalMinutes;
            numberOfTimeSlots += numberOfMinutesBetweenTakenSlots / bakingTimeInMins;

            while (bakingProgramIndex < bakingProgramsCount - 1)
            {
                BakingProgram bp = bakingPrograms[bakingProgramIndex];
                DateTime bpProgrammedAtTime = bp.BakingProgrammedAt;
                if(isLastBakingProgram(bakingProgramIndex, bakingProgramsCount))
                {
                    numberOfMinutesBetweenTakenSlots = (int)endTimeForSlots.Subtract(bpProgrammedAtTime).TotalMinutes;
                    numberOfTimeSlots += numberOfMinutesBetweenTakenSlots / bakingTimeInMins;
                    return numberOfTimeSlots;
                }
                BakingProgram nextBp = bakingPrograms[bakingProgramIndex + 1];
                DateTime nextBpProgrammedAtTime = nextBp.BakingProgrammedAt;
                numberOfMinutesBetweenTakenSlots = (int)nextBpProgrammedAtTime.Subtract(bpProgrammedAtTime).TotalMinutes;
                numberOfTimeSlots += numberOfMinutesBetweenTakenSlots / bakingTimeInMins;
                bakingProgramIndex++;

            }

            return numberOfTimeSlots;
        }

        private bool isLastBakingProgram(int bakingProgramIndex, int bakingProgramsCount)
        {
            return bakingProgramIndex == bakingProgramsCount - 1;
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

        public void CreateBakingProgram()
        {
            throw new NotImplementedException();
        }

        public void UpdateBakingProgram(BakingProgram bakingProgram)
        {
            throw new NotImplementedException();
        }
    }
}
