using SAP_API.DataAccess.Repositories;
using SAP_API.DTOs;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAP_API.Services
{
    public class ArrangingProductsToProgramsService: IArrangingProductsToProgramsService
    {

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

        private DateTime timeOrderShouldBeDone;
        private DateTime startTimeForBaking;
        private DateTime endTimeForBaking;
        private List<BakingProgram> existingProgramsProductsShouldBeArrangedTo = new List<BakingProgram>();
        private List<BakingProgram> newProgramsProductsShouldBeArrangedTo = new List<BakingProgram>();
        private Dictionary<TimeAndTempGroup, List<OrderProduct>> productsGroupedByTempAndTimeDict = new Dictionary<TimeAndTempGroup, List<OrderProduct>>();

        private readonly IBakingProgramRepository _bakingProgramRepository;
        private readonly IOvenRepository _ovenRepository;
        private readonly IProductRepository _productRepository;
        private readonly Guid _orderId;


        public ArrangingProductsToProgramsService(IBakingProgramRepository bakingProgramRepository, IOvenRepository ovenRepository, IProductRepository productRepository)
        {
            _bakingProgramRepository = bakingProgramRepository;
            _ovenRepository = ovenRepository;
            _productRepository = productRepository;
            //_orderId = orderId;
        }

        // TODO 8 and 1 should be in config
        public void SetTimeOrderShouldBeDone(DateTime timeOrderShouldBeDone)
        {
            this.timeOrderShouldBeDone = timeOrderShouldBeDone;
            startTimeForBaking = timeOrderShouldBeDone.AddHours(-8);
            endTimeForBaking = timeOrderShouldBeDone.AddHours(-1);
        }

        public void PrepareProductsForArranging(List<OrderProductRequest> orderProducts)
        {
            existingProgramsProductsShouldBeArrangedTo = new List<BakingProgram>();
            newProgramsProductsShouldBeArrangedTo = new List<BakingProgram>();
            productsGroupedByTempAndTimeDict = new Dictionary<TimeAndTempGroup, List<OrderProduct>>();
            GroupProductsByBakingTempAndTime(orderProducts);

        }
        public List<BakingProgram> GetExistingProgramsProductShouldBeArrangedInto(Guid orderId)
        {

            IEnumerable<TimeAndTempGroup> bakingGroups = productsGroupedByTempAndTimeDict.Keys;
            foreach (TimeAndTempGroup group in bakingGroups)
            {
                List<BakingProgram> bakingProgramsFromGroup = GetExistingBakingProgramsFromGroup(group);
                List<OrderProduct> productsFromGroup = productsGroupedByTempAndTimeDict[group];

                bool programsExistForGroup = bakingProgramsFromGroup.Count != 0;

                if(!programsExistForGroup)
                {
                    continue;
                }

                ArrangeProductsToExistingPrograms(bakingProgramsFromGroup, productsFromGroup, orderId);

            }

            return existingProgramsProductsShouldBeArrangedTo;
        }

        /// <summary>
        /// To avoid complicated logic, when looking for free time slots for baking, 
        /// the length of the time slot corresponds to the length of the longest baking time of 
        /// the products from the set of products in the order
        /// </summary>
        /// <param name="groupedProductsDict"></param>
        /// <returns></returns>
        public List<BakingProgram> GetNewProgramsProductsShouldBeArrangedInto(Guid orderId)
        {
            IEnumerable<TimeAndTempGroup> bakingGroups = productsGroupedByTempAndTimeDict.Keys;

            int longestBakingTimeInMins = GetLongestBakingTimeForUnarrangedProducts();
            List<BakingProgram> bakingPrograms = GetAvailableProgramsForOvens(longestBakingTimeInMins);

            foreach (TimeAndTempGroup group in bakingGroups)
            {
                List<OrderProduct> productsFromGroup = productsGroupedByTempAndTimeDict[group];
                ArrangeProductsFromGroupToNewPrograms(bakingPrograms, productsFromGroup, group, orderId);

            }
            return newProgramsProductsShouldBeArrangedTo;
        }

        public bool ThereAreProductsLeftForArranging()
        {
            IEnumerable<TimeAndTempGroup> groups = productsGroupedByTempAndTimeDict.Keys;
            foreach (TimeAndTempGroup group in groups)
            {
                List<OrderProduct> productsFromGroupToBeArranged = productsGroupedByTempAndTimeDict[group];
                if (productsFromGroupToBeArranged.Count != 0)
                {
                    return true;
                }
            }
            return false;
        }


        #region groupProducts

        private void GroupProductsByBakingTempAndTime(List<OrderProductRequest> orderProducts)
        {
            foreach (OrderProductRequest product in orderProducts)
            {
                Guid productId = (Guid)product.ProductId;
                Product productDetails = _productRepository.GetById(productId);
                TimeAndTempGroup group = GetTempAndTimeBakingGroupForProduct(productDetails);

                int quantityFromOrder = (int)product.Quantity;
                OrderProduct productFromOrder = new OrderProduct
                {
                    Product = productDetails,
                    QuantityToBake = quantityFromOrder
                };
                AddProductToGroup(group, productFromOrder);
            }
        }

        private TimeAndTempGroup GetTempAndTimeBakingGroupForProduct(Product product)
        {
            int temp = product.BakingTempInC;
            int time = product.BakingTimeInMins;
            TimeAndTempGroup group = new TimeAndTempGroup(temp, time);
            return group;
        }

        private void AddProductToGroup(TimeAndTempGroup group, OrderProduct productFromOrder)
        {
            if (productsGroupedByTempAndTimeDict.ContainsKey(group))
                productsGroupedByTempAndTimeDict[group].Add(productFromOrder);
            else
            {
                List<OrderProduct> productsFromSameGroup = new List<OrderProduct>();
                productsFromSameGroup.Add(productFromOrder);
                productsGroupedByTempAndTimeDict.Add(group, productsFromSameGroup);
            }
        }

        #endregion

        #region arragingToExistingPrograms

        //TODO: get programs between startBakingTime and endBakingTime
        private List<BakingProgram> GetExistingBakingProgramsFromGroup(TimeAndTempGroup group)
        {
            int temp = group.Temp;
            int time = group.Time;

            List<BakingProgram> bakingPrograms = _bakingProgramRepository
                .GetByTempAndTime(temp, time)
                .FindAll(p => p.RemainingOvenCapacity > 0);

            return bakingPrograms;
        }

        private void ArrangeProductsToExistingPrograms(List<BakingProgram> bakingPrograms, List<OrderProduct> products, Guid orderId)
        {
            while (ThereAreProductsLeftForArranging(products) && ThereAreProgramsLeftForArranging(bakingPrograms))
            {
                BakingProgram program = bakingPrograms.First();
                OrderProduct product = products.First();

                if (program.RemainingOvenCapacity == 0)
                {
                    FinishArrangingCurrentProgram(bakingPrograms);
                    continue;
                };

                int numberOfProductsThatCanBeArrangedToProgram = GetNumberOfProductsThatCanBeArrangedToProgram(program, product);
                
                if(numberOfProductsThatCanBeArrangedToProgram == 0)
                {
                    FinishArrangingCurrentProgram(bakingPrograms);
                    continue;
                }

                ArrangeProductToExistingProgram(product, numberOfProductsThatCanBeArrangedToProgram, program, orderId);

                if (product.QuantityToBake == 0)
                {
                    FinishArrangingCurrentProduct(products);
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

        private void ArrangeProductToExistingProgram(OrderProduct product, int numberOfProductsToArrange, BakingProgram program, Guid orderId)
        {
            existingProgramsProductsShouldBeArrangedTo.Remove(program);
            ArrangeProductToProgram(product, numberOfProductsToArrange, program, orderId);
            existingProgramsProductsShouldBeArrangedTo.Add(program);
        }

        private void ArrangeProductToProgram(OrderProduct product, int numberOfProductsToArrange, BakingProgram program, Guid orderId)
        {
            BakingProgramProduct programProduct = new BakingProgramProduct
            {
                Id = new Guid(),
                Product = product.Product,
                QuantityТoBake = numberOfProductsToArrange,
                OrderId = orderId
            };
            program.AddProductToProgram(programProduct);
            product.QuantityToBake -= numberOfProductsToArrange;
           
        }
        #endregion

        #region arragingToNewPrograms

       

        private int GetLongestBakingTimeForUnarrangedProducts()
        {
            int longestBakingTime = 0;
            IEnumerable<TimeAndTempGroup> groups = productsGroupedByTempAndTimeDict.Keys;
            foreach (TimeAndTempGroup group in groups)
            {
                List<OrderProduct> productsFromGroup = productsGroupedByTempAndTimeDict[group];
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

        // TODO programs for oven should be sorted by BakingProgrammedAt and between startTimeForBaking and endTimeForBaking
        private List<DateTime> GetAvailableTimesForBakingInOven(Oven oven, int bakingTimeInMins)
        {
            List<DateTime> availableTimes = new List<DateTime>();

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
                    Products = new List<BakingProgramProduct>(),
                    Code = Guid.NewGuid().ToString(),
                };
                ovenPrograms.Add(newBakingProgram);
            }
            return ovenPrograms;
        }
        #endregion getAvailableProgramsForOvens

        private void ArrangeProductsFromGroupToNewPrograms(List<BakingProgram> bakingPrograms, List<OrderProduct> products, TimeAndTempGroup group, Guid orderId)
        {
            while (ThereAreProductsLeftForArranging(products) && ThereAreProgramsLeftForArranging(bakingPrograms))
            {
                BakingProgram program = bakingPrograms.First();
                OrderProduct product = products.First();

                if(program.RemainingOvenCapacity == 0)
                {
                    FinishArrangingCurrentProgram(bakingPrograms);
                    continue;
                }

                int numberOfProductsThatCanBeArrangedToProgram = GetNumberOfProductsThatCanBeArrangedToProgram(program, product);

                if(numberOfProductsThatCanBeArrangedToProgram == 0)
                {
                    FinishArrangingCurrentProgram(bakingPrograms);
                    continue;
                }

                ArrangeProductToNewProgram(product, numberOfProductsThatCanBeArrangedToProgram, program, group, orderId);

                if (product.QuantityToBake == 0)
                {
                    FinishArrangingCurrentProduct(products);
                }
            }

            FinishArrangingProductsFromGroupToPrograms(bakingPrograms, group);
        }

        private void ArrangeProductToNewProgram(OrderProduct product, int numberOfProductsToArrange, BakingProgram program, TimeAndTempGroup group, Guid orderId)
        {
            program.SetTimeAndTemp(group);
            newProgramsProductsShouldBeArrangedTo.Remove(program);
            ArrangeProductToProgram(product, numberOfProductsToArrange, program, orderId);
            newProgramsProductsShouldBeArrangedTo.Add(program);
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
    }
}
