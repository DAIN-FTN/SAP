using SAP_API.DTOs;
using SAP_API.DTOs.Responses;
using SAP_API.Mappers;
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
            public int QuantityToBake { get; set; }

        }

        // TODO GetExistingProgramsProductShouldBeArrangedInto and GetNewProgramsProductsShouldBeArrangedInto 
        // should return programs with arranged products - this should be done in ArrangeProductsToPrograms
        public ArrangingResult GetExistingOrNewProgramsProductShouldBeArrangedInto(DateTime timeOrderShouldBeDone, List<OrderProductRequest> orderProducts)
        {
            List<BakingProgram> programsProductsShouldBeArrangedTo = new List<BakingProgram>();

            Dictionary<TimeAndTempGroup, List<OrderProduct>> productsGroupedByTempAndTimeDict = new Dictionary<TimeAndTempGroup, List<OrderProduct>>();
            GroupProductsByBakingTempAndTime(productsGroupedByTempAndTimeDict, orderProducts);

            List<BakingProgram> existingPrograms = GetExistingProgramsProductShouldBeArrangedInto(productsGroupedByTempAndTimeDict);
            programsProductsShouldBeArrangedTo.AddRange(existingPrograms);

            bool allProductsSuccessfullyArranged = !ThereAreProductsLeftForArranging(productsGroupedByTempAndTimeDict);
            if (allProductsSuccessfullyArranged)
            {
                return new ArrangingResult { 
                    BakingPrograms = programsProductsShouldBeArrangedTo,
                    AllProductsCanBeSuccessfullyArranged = true
                };
            }

            List<BakingProgram> newPrograms = GetNewProgramsProductsShouldBeArrangedInto(productsGroupedByTempAndTimeDict);
            programsProductsShouldBeArrangedTo.AddRange(newPrograms);

            allProductsSuccessfullyArranged = !ThereAreProductsLeftForArranging(productsGroupedByTempAndTimeDict);
            return new ArrangingResult
            {
                BakingPrograms = programsProductsShouldBeArrangedTo,
                AllProductsCanBeSuccessfullyArranged = allProductsSuccessfullyArranged
            };

        }

        #region groupProducts

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
                    QuantityToBake = quantityFromOrder
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

        #endregion


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

        #region arragingToExistingPrograms


        private List<BakingProgram> GetExistingProgramsProductShouldBeArrangedInto(Dictionary<TimeAndTempGroup, List<OrderProduct>> groupedProductsDict)
        {
            List<BakingProgram> programsProductsShouldBeArrangedTo = new List<BakingProgram>();

            IEnumerable<TimeAndTempGroup> bakingGroups = groupedProductsDict.Keys;
            foreach (TimeAndTempGroup group in bakingGroups)
            {
                List<BakingProgram> bakingProgramsFromGroup = GetExistingBakingProgramsFromGroup(group);
                List<OrderProduct> productsFromGroup = groupedProductsDict[group];

                ArrangeProductsToExistingPrograms(bakingProgramsFromGroup, productsFromGroup, programsProductsShouldBeArrangedTo);

            }

            return programsProductsShouldBeArrangedTo;
        }

        private List<BakingProgram> GetExistingBakingProgramsFromGroup(TimeAndTempGroup group)
        {
            int temp = group.Temp;
            int time = group.Time;
            List<BakingProgram> bakingPrograms = _bakingProgramRepository.GetByTempAndTime(temp, time);
            return bakingPrograms;
        }

        private void ArrangeProductsToExistingPrograms(List<BakingProgram> bakingPrograms, List<OrderProduct> products, List<BakingProgram> programsProductsShouldBeArrangedTo)
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
                    ArrangeProductToProgram(product, numberOfProductsThatCanBeArrangedToProgram, program, programsProductsShouldBeArrangedTo);

                    if (product.QuantityToBake == 0)
                    {
                        FinishArrangingCurrentProduct(products);
                    }
                }
            }
        }


        #endregion

        #region arrangingMethods
        private bool ThereAreProductsLeftForArranging(List<OrderProduct> products)
        {
            return products.Count != 0;
        }

        private bool ThereAreProgramsLeftForArranging(List<BakingProgram> bakingPrograms)
        {
            return bakingPrograms.Count != 0;
        }

        private int GetNumberOfProductsThatCanBeArrangedToProgram(BakingProgram program, OrderProduct product)
        {
            int size = product.Product.Size;
            int quantityFromOrder = product.QuantityToBake;
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

        private void ArrangeProductToProgram(OrderProduct product, int numberOfProductsToArrange, BakingProgram program, List<BakingProgram> programsProductsShouldBeArrangedTo)
        {
            programsProductsShouldBeArrangedTo.Remove(program);
            BakingProgramProduct programProduct = new BakingProgramProduct
            {
                Id = new Guid(),
                Product = product.Product,
                Order = null,
                QuantityТoBake = numberOfProductsToArrange
            };
            program.AddProductToProgram(programProduct);
            product.QuantityToBake -= numberOfProductsToArrange;
            programsProductsShouldBeArrangedTo.Add(program);
        }
        #endregion

        #region arragingToNewPrograms

        /// <summary>
        /// To avoid complicated logic, when looking for free time slots for baking, 
        /// the length of the time slot corresponds to the length of the longest baking time of 
        /// the products from the set of products in the order
        /// </summary>
        /// <param name="groupedProductsDict"></param>
        /// <returns></returns>
        private List<BakingProgram> GetNewProgramsProductsShouldBeArrangedInto(Dictionary<TimeAndTempGroup, List<OrderProduct>> groupedProductsDict)
        {
            List<BakingProgram> programsProductsShouldBeArrangedTo = new List<BakingProgram>();
            IEnumerable<TimeAndTempGroup> bakingGroups = groupedProductsDict.Keys;

            int longestBakingTimeInMins = GetLongestBakingTimeForUnarrangedProducts(groupedProductsDict);
            List<BakingProgram> bakingPrograms = GetAvailableProgramsForOvens(longestBakingTimeInMins);

            foreach (TimeAndTempGroup group in bakingGroups)
            {
                List<OrderProduct> productsFromGroup = groupedProductsDict[group];
                ArrangeProductsFromGroupToNewPrograms(bakingPrograms, productsFromGroup, group, programsProductsShouldBeArrangedTo);

            }
            return programsProductsShouldBeArrangedTo;
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
                    int bakingTimeForProducts = group.Time;
                    if (longestBakingTime < bakingTimeForProducts)
                        longestBakingTime = bakingTimeForProducts;

                }
            }

            return longestBakingTime;
        }

        #region getAvailableProgramsForOvens

        private List<BakingProgram> GetAvailableProgramsForOvens(int bakingDurationInMins)
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

        // TODO productsShouldBeDoneAtTime should be param, 8 and 1 should be in config
        // TODO pograms for oven should be sorted by BakingProgrammedAt and between startTimeForBaking and endTimeForBaking
        private List<DateTime> GetAvailableTimesForBakingInOven(Oven oven, int bakingTimeInMins)
        {
            List<DateTime> availableTimes = new List<DateTime>();

            DateTime productsShouldBeDoneAtTime = new DateTime();
            DateTime startTimeForBaking = productsShouldBeDoneAtTime.AddHours(-8);
            DateTime endTimeForBaking = productsShouldBeDoneAtTime.AddHours(-1);

            List<BakingProgram> existingBakingProgramsForOven = _bakingProgramRepository.GetByOvenId(oven.Id);

            if (OvenIsAvailableForTheWholeTimePeriod(existingBakingProgramsForOven))
            {
                List<DateTime> times = GetAvailableTimesForBakingBetweenStartAndEndTime(startTimeForBaking, endTimeForBaking, bakingTimeInMins);
                availableTimes.AddRange(times);
                return availableTimes;
            }

            for (int i = 0; i < existingBakingProgramsForOven.Count; i++)
            {
                BakingProgram currentProgram = existingBakingProgramsForOven[i];
                DateTime currentProgramBakingEndTime = currentProgram.BakingProgrammedAt.AddMinutes(bakingTimeInMins);
                if (CurrentProgramIsTheLatestProgram(currentProgram, existingBakingProgramsForOven))
                {
                    List<DateTime> timesBetweenLatestProgramAndEndTime = GetAvailableTimesForBakingBetweenStartAndEndTime(currentProgramBakingEndTime, endTimeForBaking, bakingTimeInMins);
                    availableTimes.AddRange(timesBetweenLatestProgramAndEndTime);
                    continue;
                }

                BakingProgram adjecentProgram = existingBakingProgramsForOven[i + 1];
                DateTime adjecentProgramBakingStartTime = adjecentProgram.BakingProgrammedAt;
                List<DateTime> timesBetweenAdjecentPrograms = GetAvailableTimesForBakingBetweenStartAndEndTime(currentProgramBakingEndTime, adjecentProgramBakingStartTime, bakingTimeInMins);
                availableTimes.AddRange(timesBetweenAdjecentPrograms);

            }

            return availableTimes;
        }

        private bool OvenIsAvailableForTheWholeTimePeriod(List<BakingProgram> existingBakingProgramsForOven)
        {
            return existingBakingProgramsForOven.Count == 0;
        }

        private bool CurrentProgramIsTheLatestProgram(BakingProgram currentProgram, List<BakingProgram> existingBakingProgramsForOven)
        {
            int indexOfCurrentProgram = existingBakingProgramsForOven.IndexOf(currentProgram);
            return indexOfCurrentProgram == existingBakingProgramsForOven.Count - 1;
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
            while (i < numberOfBakingSlotsBetweenTimes)
            {
                availableTimesForBaking.Add(startTimeOfNextBakingSlot);
                startTimeOfNextBakingSlot = startTimeOfNextBakingSlot.AddMinutes(bakingTimeInMins);
                i++;
            }
            return availableTimesForBaking;
        }

        private List<BakingProgram> MakeOvenProgramsAtAvailableTimes(Oven oven, List<DateTime> availableTimes)
        {
            List<BakingProgram> ovenPrograms = new List<BakingProgram>();
            foreach (DateTime time in availableTimes)
            {
                BakingProgram newBakingProgram = new BakingProgram
                {
                    Status = BakingProgramStatus.Pending,
                    RemainingOvenCapacity = oven.Capacity,
                    Oven = oven,
                    BakingProgrammedAt = time,
                    Products = new List<BakingProgramProduct>()
                };
                ovenPrograms.Add(newBakingProgram);
            }
            return ovenPrograms;
        }
        #endregion getAvailableProgramsForOvens

        private void ArrangeProductsFromGroupToNewPrograms(List<BakingProgram> bakingPrograms, List<OrderProduct> products, TimeAndTempGroup group, List<BakingProgram> programsProductsShouldBeArrangedTo)
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
                    ArrangeProductToNewProgram(product, numberOfProductsThatCanBeArrangedToProgram, program, group, programsProductsShouldBeArrangedTo);

                    if (product.QuantityToBake == 0)
                    {
                        FinishArrangingCurrentProduct(products);
                    }
                }
            }

            FinishArrangingProductsFromGroupToPrograms(bakingPrograms, group);
        }

        private void ArrangeProductToNewProgram(OrderProduct product, int numberOfProductsToArrange, BakingProgram program, TimeAndTempGroup group, List<BakingProgram> programsProductsShouldBeArrangedTo)
        {
            program.SetTimeAndTemp(group);
            ArrangeProductToProgram(product, numberOfProductsToArrange, program, programsProductsShouldBeArrangedTo);
        }

        /// <summary>
        /// If program was used to arrange products from one group, when arranging next group
        /// this program shouldn't be used because it is already programmed for a specific group
        /// </summary>
        /// <param name="bakingPrograms"></param>
        /// <param name="group"></param>
        private void FinishArrangingProductsFromGroupToPrograms(List<BakingProgram> bakingPrograms, TimeAndTempGroup group)
        {
            if (bakingPrograms.Count > 0)
            {
                BakingProgram lastProgramAfterArranging = bakingPrograms[0];
                if (ProductsFromGroupWereArrangedToProgram(lastProgramAfterArranging, group))
                    bakingPrograms.Remove(lastProgramAfterArranging);
            }
        }

        private bool ProductsFromGroupWereArrangedToProgram(BakingProgram lastProgramAfterArranging, TimeAndTempGroup group)
        {
            TimeAndTempGroup programGroup = GetProgramGroup(lastProgramAfterArranging);
            return programGroup.Equals(group);
        }

        private TimeAndTempGroup GetProgramGroup(BakingProgram program)
        {
            int temp = program.BakingTempInC;
            int time = program.BakingTimeInMins;
            return new TimeAndTempGroup(temp, time);
        }

        #endregion


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
            ArrangingResult result = GetExistingOrNewProgramsProductShouldBeArrangedInto(body.ShouldBeDoneAt, body.OrderProducts);
            List<BakingProgram> listToMap = result.BakingPrograms;
            List<BakingProgramResponse> resultList = BakingProgramMapper.CreateListOfBakingProgramResponse(listToMap);
            return resultList;
        }
    }
}
