using SAP_API.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAP_API.DataAccess.Repositories
{
    public class BakingProgramRepository : IBakingProgramRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<BakingProgram> _bakingPrograms;

        public BakingProgramRepository(DbContext context)
        {
            this._context = context;
            this._bakingPrograms = context.Set<BakingProgram>();
        }

        public IEnumerable<BakingProgram> GetAll()
        {
            return _bakingPrograms.ToList();
        }

        public BakingProgram Update(BakingProgram bakingProgram)
        {
            var existingBakingProgram = _bakingPrograms.FirstOrDefault(x => x.Id == bakingProgram.Id);
            if (existingBakingProgram != null)
            {
                _bakingPrograms.Remove(existingBakingProgram);
                _bakingPrograms.Add(bakingProgram);

                _context.SaveChanges();
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
            _context.SaveChanges();

            return bakingProgram;
        }

        public bool Delete(Guid id)
        {
            var bakingProgram = _bakingPrograms.FirstOrDefault(x => x.Id == id);
            if (bakingProgram != null)
            {
                _bakingPrograms.Remove(bakingProgram);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public List<BakingProgram> GetByTempAndTime(int temp, int time)
        {
            return _bakingPrograms.Where(x => x.BakingTempInC == temp && x.BakingTimeInMins == time).ToList();
        }

        public List<BakingProgram> GetByOvenId(Guid ovenId)
        {
            return _bakingPrograms.Where(x => x.Oven.Id.Equals(ovenId)).ToList();
        }

        public List<BakingProgram> GetProgramsWithBakingProgrammedAtBetweenDateTimes(DateTime startTime, DateTime endTime)
        {
            return _bakingPrograms.Where(x => DateTime.Compare(startTime, x.BakingProgrammedAt) <= 0 && DateTime.Compare(endTime, x.BakingProgrammedAt) >= 0).ToList();
        }
    }
}
