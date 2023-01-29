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
                    Status = BakingPogramStatus.Created,
                    BakingTimeInMins = 60,
                    BakingTimeInC = 180,
                    BakingProgrammedAt = new DateTime(2020, 1, 1, 12, 0, 0),
                    BakingStartedAt = new DateTime(2020, 1, 1, 12, 0, 0),
                    Oven = new Oven { Id = new Guid("00000000-0000-0000-0000-000000000001"), Code = "Oven 1" },
                    PreparedBy = new User { Id = new Guid("00000000-0000-0000-0000-000000000002"), Username = "John", Password = "Doe" },
                    RemainingOvenCapacity = 2
                },
                new BakingProgram
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    CreatedAt = new DateTime(2020, 2, 1, 12, 0, 0),
                    Status = BakingPogramStatus.Created,
                    BakingTimeInMins = 90,
                    BakingTimeInC = 200,
                    BakingProgrammedAt = new DateTime(2020, 2, 1, 12, 0, 0),
                    BakingStartedAt = new DateTime(2020, 2, 1, 12, 30, 0),
                    Oven = new Oven { Id = new Guid("00000000-0000-0000-0000-000000000002"), Code = "Oven 2" },
                    PreparedBy = new User { Id = new Guid("00000000-0000-0000-0000-000000000003"), Username = "Jane", Password = "Doe" },
                    RemainingOvenCapacity = 1
                },
                new BakingProgram
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    CreatedAt = new DateTime(2020, 3, 1, 12, 0, 0),
                    Status = BakingPogramStatus.Created,
                    BakingTimeInMins = 120,
                    BakingTimeInC = 190,
                    BakingProgrammedAt = new DateTime(2020, 3, 1, 12, 0, 0),
                    BakingStartedAt = new DateTime(2020, 3, 1, 12, 45, 0),
                    Oven = new Oven { Id = new Guid("00000000-0000-0000-0000-000000000003"), Code = "Oven 3" },
                    PreparedBy = new User { Id = new Guid("00000000-0000-0000-0000-000000000004"), Username = "Bob", Password = "Smith" }
                }
            };
        }
    }
}
