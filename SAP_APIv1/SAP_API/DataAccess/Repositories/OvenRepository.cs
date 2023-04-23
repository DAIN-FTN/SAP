using SAP_API.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAP_API.DataAccess.DbContexts;

namespace SAP_API.DataAccess.Repositories
{
    public class OvenRepository : IOvenRepository
    {
        private readonly BakeryContext _context;
        private readonly DbSet<Oven> _ovens;

        public OvenRepository(BakeryContext context)
        {
            this._context = context;
            this._ovens = context.Set<Oven>();
        }

        public IEnumerable<Oven> GetAll()
        {
            return _ovens
                .ToList();
        }

        public Oven GetById(Guid id)
        {
            return _ovens
                .SingleOrDefault(o => o.Id == id);
        }

        public Oven Create(Oven entity)
        {
            _ovens.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public Oven Update(Oven entity)
        {
            var oven = GetById(entity.Id);
            if (oven != null)
            {
                oven.Code = entity.Code;
                oven.MaxTempInC = entity.MaxTempInC;
                oven.Capacity = entity.Capacity;

                _context.SaveChanges();
            }

            throw new Exception("Oven not found");
        }

        public bool Delete(Guid id)
        {
            var oven = GetById(id);
            if (oven != null)
            {
                _ovens.Remove(oven);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

    }
}
