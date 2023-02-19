using SAP_API.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SAP_API.Repositories
{
    public class BakingProgramRepository : IBakingProgramRepository
    {
        private List<BakingProgram> _bakingPrograms = new List<BakingProgram>();

        public BakingProgramRepository()
        {
            SeedData();
        }

        public IEnumerable<BakingProgram> GetAll()
        {
            return _bakingPrograms;
        }

        public BakingProgram Update(BakingProgram bakingProgram)
        {
            var existingBakingProgram = _bakingPrograms.FirstOrDefault(x => x.Id == bakingProgram.Id);
            if (existingBakingProgram != null)
            {
                _bakingPrograms.Remove(existingBakingProgram);
                _bakingPrograms.Add(bakingProgram);
            }
            return bakingProgram;
        }

        public BakingProgram GetById(Guid id)
        {
            return _bakingPrograms.FirstOrDefault(x => x.Id == id);
        }

        public BakingProgram Create(BakingProgram bakingProgram)
        {
            _bakingPrograms.Add(bakingProgram);
            return bakingProgram;
        }

        public bool Delete(Guid id)
        {
            var bakingProgram = _bakingPrograms.FirstOrDefault(x => x.Id == id);
            if (bakingProgram != null)
            {
                return _bakingPrograms.Remove(bakingProgram);
            }
            return false;
        }

        private void SeedData()
        {
            _bakingPrograms = new List<BakingProgram>
            {
                new BakingProgram
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000000"),
                    CreatedAt = new DateTime(2020, 1, 1, 12, 0, 0),
                    Status = BakingProgramStatus.Created,
                    BakingTimeInMins = 30,
                    BakingTempInC = 120,
                    BakingProgrammedAt = DateTime.Now,
                    BakingStartedAt = null,
                    Oven = new Oven { Id = new Guid("00000000-0000-0000-0000-000000000001"), Code = "Oven 1" },
                    PreparedBy = new User { Id = new Guid("00000000-0000-0000-0000-000000000002"), Username = "John", Password = "Doe" },
                    RemainingOvenCapacity = 10,
                    Products = new List<BakingProgramProduct>()
                    {
                        new BakingProgramProduct
                        {
                            Id = Guid.NewGuid(),
                             Product = new Product
                            {
                                BakingTempInC = 120,
                                BakingTimeInMins = 30,
                                Id = Guid.Parse("5cd54cb6-0df4-420f-96fd-f6e2cf6e2000"),
                                Name = "Chocolate Croissant",
                                Size = 2
                            },
                            Order = new Order
                            {
                                Id = new Guid("00000000-0000-0000-0000-000000000000"),
                                ShouldBeDoneAt = new DateTime(2023, 2, 4, 12, 0, 0),
                                Status = OrderStatus.Confirmed,
                                Customer = new Customer()
                            },
                            QuantityТoBake = 6
                        },
                        new BakingProgramProduct
                        {
                            Id = Guid.NewGuid(),
                            Product =  new Product
                            {
                                BakingTempInC = 120,
                                BakingTimeInMins= 30,
                                Id= Guid.Parse("d174996a-63e4-4b6b-b322-fdf235d91444"),
                                Name = "Pizza",
                                Size = 6
                            },
                            Order = new Order
                            {
                                Id = new Guid("00000000-0000-0000-0000-000000000000"),
                                ShouldBeDoneAt = new DateTime(2023, 2, 4, 12, 0, 0),
                                Status = OrderStatus.Confirmed,
                                Customer = new Customer()
                            },
                            QuantityТoBake = 4
                        }
                    }
                },
                new BakingProgram
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    CreatedAt = new DateTime(2020, 2, 1, 12, 0, 0),
                    Status = BakingProgramStatus.Created,
                    BakingTimeInMins = 30,
                    BakingTempInC = 140,
                    BakingProgrammedAt =  DateTime.Now,
                    BakingStartedAt = null,
                    Oven = new Oven { Id = new Guid("00000000-0000-0000-0000-000000000002"), Code = "Oven 2" },
                    PreparedBy = new User { Id = new Guid("00000000-0000-0000-0000-000000000003"), Username = "Jane", Password = "Doe" },
                    RemainingOvenCapacity = 10,
                    Products = new List<BakingProgramProduct>() {
                        new BakingProgramProduct
                        {
                            Id = Guid.NewGuid(),
                             Product = new Product
                            {
                                BakingTempInC = 120,
                                BakingTimeInMins = 30,
                                Id = Guid.Parse("5cd54cb6-0df4-420f-96fd-f6e2cf6e2000"),
                                Name = "Chocolate Croissant",
                                Size = 2
                            },
                            Order = new Order
                            {
                                Id = new Guid("00000000-0000-0000-0000-000000000000"),
                                ShouldBeDoneAt = new DateTime(2023, 2, 4, 12, 0, 0),
                                Status = OrderStatus.Confirmed,
                                Customer = new Customer()
                            },
                            QuantityТoBake = 4
                        },
                        new BakingProgramProduct
                        {
                            Id = Guid.NewGuid(),
                            Product =  new Product
                            {
                                BakingTempInC = 120,
                                BakingTimeInMins= 30,
                                Id= Guid.Parse("d174996a-63e4-4b6b-b322-fdf235d91444"),
                                Name = "Pizza",
                                Size = 6
                            },
                            Order = new Order
                            {
                                Id = new Guid("00000000-0000-0000-0000-000000000000"),
                                ShouldBeDoneAt = new DateTime(2023, 2, 4, 12, 0, 0),
                                Status = OrderStatus.Confirmed,
                                Customer = new Customer()
                            },
                            QuantityТoBake = 6
                        }
                    }
                },
                new BakingProgram
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    CreatedAt = new DateTime(2020, 3, 1, 12, 0, 0),
                    Status = BakingProgramStatus.Done,
                    BakingTimeInMins = 120,
                    BakingTempInC = 190,
                    BakingProgrammedAt = DateTime.Now,
                    BakingStartedAt = new DateTime(2023, 12, 2, 11, 5, 0),
                    Oven = new Oven { Id = new Guid("00000000-0000-0000-0000-000000000001"), Code = "Oven 1" },
                    PreparedBy = new User { Id = new Guid("00000000-0000-0000-0000-000000000004"), Username = "Bob", Password = "Smith" },
                    RemainingOvenCapacity = 10,
                    Products = new List<BakingProgramProduct>()
                }
            };
        }

        public List<BakingProgram> GetByTempAndTime(int temp, int time)
        {
            return _bakingPrograms.FindAll(x => x.BakingTempInC == temp && x.BakingTimeInMins == time);
        }

        public List<BakingProgram> GetByOvenId(Guid ovenId)
        {
            return _bakingPrograms.FindAll(x => x.Oven.Id.Equals(ovenId));
        }

        public List<BakingProgram> GetProgramsWithBakingProgrammedAtBetweenDateTimes(DateTime startTime, DateTime endTime)
        {
            return _bakingPrograms.FindAll(x => DateTime.Compare(startTime, x.BakingProgrammedAt) <= 0 && DateTime.Compare(endTime, x.BakingProgrammedAt) >= 0);
        }
    }
}
