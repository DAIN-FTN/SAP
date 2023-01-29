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

        internal class TimeAndTempGroup
        {
            public string Group { get; }
            public int Temp { get; set; }
            public int Time { get; set; }


            public TimeAndTempGroup(int temp, int time)
            {
                Group = temp + "_" + time;
                Temp = temp;
                Time = time;

            }

            public override bool Equals(object obj)
            {
                if (obj is TimeAndTempGroup)
                    return ((TimeAndTempGroup)obj).Group.Equals(this.Group);
                return false;
            }

            public override int GetHashCode()
            {
                return this.Group.GetHashCode();
            }

        }

        internal class OrderProduct
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }

        }

        // TODO GetExsistingProgramsProductShouldBeArrangedInto and GetNewProgramsProductsShouldBeArrangedInto 
        // should return programs with arranged products - this should be done in ArrangeProductsToPrograms
        public Tuple<bool, List<BakingProgram>> GetExsistingOrNewProgramsProductShouldBeArrangedInto(DateTime timeOrderShouldBeDone, List<OrderProductRequest> orderProducts)
        {
            List<BakingProgram> programsProductsShouldBeArrangedTo = new List<BakingProgram>();

            Dictionary<TimeAndTempGroup, List<OrderProduct>> productsGroupedByTempAndTimeDict = new Dictionary<TimeAndTempGroup, List<OrderProduct>>();
            GroupProductsByBakingTempAndTime(productsGroupedByTempAndTimeDict, orderProducts);

            List<BakingProgram> exsistingPrograms = GetExsistingProgramsProductShouldBeArrangedInto(productsGroupedByTempAndTimeDict);
            programsProductsShouldBeArrangedTo.AddRange(exsistingPrograms);

            bool allProductsSuccessfullyArranged = !ThereAreProductsLeftForArranging(productsGroupedByTempAndTimeDict);
            if (allProductsSuccessfullyArranged)
            {
                return new Tuple<bool, List<BakingProgram>>(true, programsProductsShouldBeArrangedTo);
            }

            List<BakingProgram> newPrograms = GetNewProgramsProductsShouldBeArrangedInto(productsGroupedByTempAndTimeDict);
            programsProductsShouldBeArrangedTo.AddRange(newPrograms);

            allProductsSuccessfullyArranged = !ThereAreProductsLeftForArranging(productsGroupedByTempAndTimeDict);
            return new Tuple<bool, List<BakingProgram>>(allProductsSuccessfullyArranged, programsProductsShouldBeArrangedTo);

        }

        private void GroupProductsByBakingTempAndTime(Dictionary<TimeAndTempGroup, List<OrderProduct>> groupedProductsDict, List<OrderProductRequest> orderProducts)
        {
            foreach (OrderProductRequest product in orderProducts)
            {
                Guid productId = product.ProductId;
                Product productDetails = _productRepository.GetById(productId);
                TimeAndTempGroup group = GetTempAndTimeBakingGroupForProduct(productDetails);

                int quantityFromOrder = product.Quantity;
                OrderProduct productFromOrder = new OrderProduct
                {
                    Product = productDetails,
                    Quantity = quantityFromOrder
                };
                AddProductToGroup(groupedProductsDict, group, productFromOrder);
            }
        }

        private TimeAndTempGroup GetTempAndTimeBakingGroupForProduct(Product product)
        {
            int temp = product.BakingTempInC;
            int time = product.BakingTimeInMins;
            TimeAndTempGroup group = new TimeAndTempGroup(temp, time);
            return group;
        }

        private void AddProductToGroup(Dictionary<TimeAndTempGroup, List<OrderProduct>> groupedProductsDict, TimeAndTempGroup group, OrderProduct productFromOrder)
        {
            if (groupedProductsDict.ContainsKey(group))
                groupedProductsDict[group].Add(productFromOrder);
            else
            {
                List<OrderProduct> productsFromSameGroup = new List<OrderProduct>();
                productsFromSameGroup.Add(productFromOrder);
                groupedProductsDict.Add(group, productsFromSameGroup);
            }
        }

        private List<BakingProgram> GetExsistingProgramsProductShouldBeArrangedInto(Dictionary<TimeAndTempGroup, List<OrderProduct>> groupedProductsDict)
        {
            List<BakingProgram> programs = new List<BakingProgram>();

            IEnumerable<TimeAndTempGroup> bakingGroups = groupedProductsDict.Keys;
            foreach (TimeAndTempGroup group in bakingGroups)
            {
                List<BakingProgram> bakingProgramsFromGroup = GetExistingBakingProgramsFromGroup(group);
                List<OrderProduct> productsFromGroup = groupedProductsDict[group];

                ArrangeProductsToPrograms(bakingProgramsFromGroup, productsFromGroup);

            }

            return programs;
        }


        private List<BakingProgram> GetExistingBakingProgramsFromGroup(TimeAndTempGroup group)
        {
            int temp = group.Temp;
            int time = group.Time;
            List<BakingProgram> bakingPrograms = _bakingProgramRepository.GetByTempAndTime(temp, time);
            return bakingPrograms;
        }

        private void ArrangeProductsToPrograms(List<BakingProgram> bakingPrograms, List<OrderProduct> products)
        {
            int currentProgramIndex = 0;
            int currentProductIndex = 0;
            while (ThereAreProductsLeftForArranging(products) && ThereAreProgramsLeftForArranging(bakingPrograms))
            {
                BakingProgram program = bakingPrograms[currentProgramIndex];
                bakingPrograms.RemoveAt(currentProgramIndex);

                while (program.RemainingOvenCapacity > 0 && ThereAreProductsLeftForArranging(products))
                {
                    OrderProduct product = products[currentProductIndex];
                    int numberOfProductsThatCanBeArrangedToProgram = GetNumberOfProductsThatCanBeArrangedToProgram(program, product);
                    if (numberOfProductsThatCanBeArrangedToProgram == 0)
                    {
                        FinishArrangingCurrentProgram(bakingPrograms);
                        break;
                    }
                    ArrangeProductToProgram(product, numberOfProductsThatCanBeArrangedToProgram, program);

                    if (product.Quantity == 0)
                    {
                        FinishArrangingCurrentProduct(products);
                    }
                }
            }
        }

        private bool ThereAreProductsLeftForArranging(List<OrderProduct> products)
        {
            return products.Count != 0;
        }

        private bool ThereAreProductsLeftForArranging(Dictionary<TimeAndTempGroup, List<OrderProduct>> groupedProductsDict)
        {
            IEnumerable<TimeAndTempGroup> groups = groupedProductsDict.Keys;
            foreach (TimeAndTempGroup group in groups)
            {
                List<OrderProduct> productsFromGroupToBeArranged = groupedProductsDict[group];
                if (productsFromGroupToBeArranged.Count != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ThereAreProgramsLeftForArranging(List<BakingProgram> bakingPrograms)
        {
            return bakingPrograms.Count != 0;
        }

        private int GetNumberOfProductsThatCanBeArrangedToProgram(BakingProgram program, OrderProduct product)
        {
            int size = product.Product.Size;
            int quantityFromOrder = product.Quantity;
            int remainingCapacity = program.RemainingOvenCapacity;
            int numberOfProductsFittingInOven = remainingCapacity / size;
            int numberOfProductsThatCanBeArranged = Math.Min(quantityFromOrder, numberOfProductsFittingInOven);
            return numberOfProductsThatCanBeArranged;
        }

        private void FinishArrangingCurrentProgram(List<BakingProgram> bakingPrograms)
        {
            bakingPrograms.RemoveAt(0);
        }

        private void FinishArrangingCurrentProduct(List<OrderProduct> products)
        {
            products.RemoveAt(0);
        }

        private void ArrangeProductToProgram(OrderProduct product, int numberOfProductsToArrange, BakingProgram program)
        {
            BakingProgramProduct programProduct = new BakingProgramProduct
            {
                Id = new Guid(),
                Product = product.Product,
                Order = null,
                Quantity = numberOfProductsToArrange
            };
            program.AddProductToProgram(programProduct);
            product.Quantity -= numberOfProductsToArrange;
        }



        private List<BakingProgram> GetNewProgramsProductsShouldBeArrangedInto(Dictionary<TimeAndTempGroup, List<OrderProduct>> groupedProductsDict)
        {
            List<BakingProgram> programs = new List<BakingProgram>();
            IEnumerable<TimeAndTempGroup> bakingGroups = groupedProductsDict.Keys;

            
            // to avoid complicated logic, when searching for available time slots,
            // the lenght of the slot is represented by the longest baking time from all of the products
            int longestBakingTimeInMins = GetLongestBakingTimeForUnarrangedProducts(groupedProductsDict);
            List<BakingProgram> bakingPrograms = GetAvailableBakingProgramsForOvens(longestBakingTimeInMins);

            foreach (TimeAndTempGroup group in bakingGroups)
            {
                List<OrderProduct> productsFromGroup = groupedProductsDict[group];
                ArrangeProductsToPrograms(bakingPrograms, productsFromGroup);

            }
            return programs;
        }

        private int GetLongestBakingTimeForUnarrangedProducts(Dictionary<TimeAndTempGroup, List<OrderProduct>> groupedProductsDict)
        {
            int longestBakingTime = 0;
            IEnumerable<TimeAndTempGroup> groups = groupedProductsDict.Keys;
            foreach (TimeAndTempGroup group in groups)
            {
                List<OrderProduct> productsFromGroup = groupedProductsDict[group];
                if (productsFromGroup.Count != 0)
                {
                    foreach(OrderProduct orderProduct in productsFromGroup)
                    {
                        int bakingTime = orderProduct.Product.BakingTimeInMins;
                        if (longestBakingTime < bakingTime)
                            longestBakingTime = bakingTime;
                    }
                }
            }

            return longestBakingTime;
        }

        private List<BakingProgram> GetAvailableBakingProgramsForOvens(int bakingDurationInMins)
        {
            List<BakingProgram> availableBakingPrograms = new List<BakingProgram>();
            List<Oven> ovens = (List<Oven>)_ovenRepository.GetAll();
            foreach (Oven oven in ovens)
            {

                List<BakingProgram> ovenBakingPrograms = GetAvailableOvenPrograms(oven, bakingDurationInMins);
                availableBakingPrograms.AddRange(ovenBakingPrograms);
            }
            return availableBakingPrograms;
        }

        private List<BakingProgram> GetAvailableOvenPrograms(Oven oven, int bakingDurationInMins)
        {
            List<DateTime> availableTimes = GetAvailableTimesForBakingInOven(oven, bakingDurationInMins);
            List<BakingProgram> availableBakingPrograms = MakeOvenProgramsAtAvailableTimes(oven, availableTimes);
            return availableBakingPrograms;
        }

        private List<BakingProgram> MakeOvenProgramsAtAvailableTimes(Oven oven, List<DateTime> availableTimes)
        {
            List<BakingProgram> ovenPrograms = new List<BakingProgram>();
            foreach(DateTime time in availableTimes)
            {
                BakingProgram newBakingProgram = new BakingProgram
                {
                    Status = BakingProgramStatus.Pending,
                    RemainingOvenCapacity = oven.Capacity,
                    Oven = oven,
                    BakingProgrammedAt = time
                };
                ovenPrograms.Add(newBakingProgram);
            }
            return ovenPrograms;
        }

        private List<DateTime> GetAvailableTimesForBakingInOven(Oven oven, int bakingTimeInMins)
        {
            List<DateTime> availableTimes = new List<DateTime>();

            // TODO productsShouldBeDoneAtTime should be param, 8 and 1 should be in config
            DateTime productsShouldBeDoneAtTime = new DateTime();
            DateTime startTimeForBaking = productsShouldBeDoneAtTime.AddHours(-8);
            DateTime endTimeForBaking = productsShouldBeDoneAtTime.AddHours(-1);

            // TODO programs should be sorted by ProgramedAt
            List<BakingProgram> exsistingBakingProgramsForOven = _bakingProgramRepository.GetByOvenId(oven.Id);

            if (OvenIsAvailableForTheWholeTimePeriod(exsistingBakingProgramsForOven))
            {
                List<DateTime> times = GetAvailableTimesForBakingBetweenStartAndEndTime(startTimeForBaking, endTimeForBaking, bakingTimeInMins);
                availableTimes.AddRange(times);
                return availableTimes;
            }

            for(int i=0; i < exsistingBakingProgramsForOven.Count; i++)
            {
                BakingProgram currentProgram = exsistingBakingProgramsForOven[i];
                DateTime currentProgramBakingEndTime = currentProgram.BakingProgrammedAt.AddMinutes(bakingTimeInMins);
                if (CurrentProgramIsTheLatestProgram(currentProgram, exsistingBakingProgramsForOven))
                {
                    
                    List<DateTime> timesBetweenLatestProgramAndEndTime = GetAvailableTimesForBakingBetweenStartAndEndTime(currentProgramBakingEndTime, endTimeForBaking, bakingTimeInMins);
                    availableTimes.AddRange(timesBetweenLatestProgramAndEndTime);
                    continue;
                }

                BakingProgram adjecentProgram = exsistingBakingProgramsForOven[i + 1];
                DateTime adjecentProgramBakingStartTime = adjecentProgram.BakingProgrammedAt;
                List<DateTime> timesBetweenAdjecentPrograms = GetAvailableTimesForBakingBetweenStartAndEndTime(currentProgramBakingEndTime, adjecentProgramBakingStartTime, bakingTimeInMins);
                availableTimes.AddRange(timesBetweenAdjecentPrograms);

            }

            return availableTimes;
        }

        private bool OvenIsAvailableForTheWholeTimePeriod(List<BakingProgram> exsistingBakingProgramsForOven)
        {
            return exsistingBakingProgramsForOven.Count == 0;
        }

        private bool CurrentProgramIsTheLatestProgram(BakingProgram currentProgram, List<BakingProgram> exsistingBakingProgramsForOven)
        {
            int indexOfCurrentProgram = exsistingBakingProgramsForOven.IndexOf(currentProgram);
            return indexOfCurrentProgram == exsistingBakingProgramsForOven.Count - 1;
        }


        private List<DateTime> GetAvailableTimesForBakingBetweenStartAndEndTime(DateTime startTimeForBaking, DateTime endTimeForBaking, int bakingTimeInMins)
        {
            List<DateTime> availableTimesForBaking = new List<DateTime>();
            int numberOfMinutesBetweenTimes = (int)endTimeForBaking.Subtract(startTimeForBaking).TotalMinutes;
            int numberOfBakingSlotsBetweenTimes = numberOfMinutesBetweenTimes / bakingTimeInMins;
            if (numberOfBakingSlotsBetweenTimes == 0)
                return availableTimesForBaking;
            
            int i = 0;
            DateTime startTimeOfNextBakingSlot = startTimeForBaking;
            while(i < numberOfBakingSlotsBetweenTimes)
            {
                availableTimesForBaking.Add(startTimeOfNextBakingSlot);
                startTimeOfNextBakingSlot = startTimeOfNextBakingSlot.AddMinutes(bakingTimeInMins);
                i++;
            }
            return availableTimesForBaking;
        }

        public void CreateBakingProgram()
        {
            throw new NotImplementedException();
        }

        public void UpdateBakingProgram(BakingProgram bakingProgram)
        {
            throw new NotImplementedException();
        }

        public List<BakingProgramResponse> FindAvailableBakingPrograms(FindAvailableBakingProgramsRequest body)
        {
            List<BakingProgramResponse> resultList = new List<BakingProgramResponse>();
            // TODO get response
            // Tuple<bool, List<BakingProgram>> result = GetExsistingOrNewProgramsProductShouldBeArrangedInto(body.ShouldBeDoneAt, body.OrderProducts);
            return resultList;
        }
    }
}
