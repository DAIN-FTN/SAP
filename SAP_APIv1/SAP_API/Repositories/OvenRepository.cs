using SAP_API.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SAP_API.Repositories
{
    public class OvenRepository: IOvenRepository
    {
        private List<Oven> _ovens = new List<Oven>();

        public OvenRepository()
        {
            SeedData();
        }

        public IEnumerable<Oven> GetAll()
        {
            return _ovens;
        }

        public Oven GetById(Guid id)
        {
            return _ovens.SingleOrDefault(o => o.Id == id);
        }

        public Oven Create(Oven entity)
        {
            _ovens.Add(entity);
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
            }

            return oven;
        }

        public bool Delete(Guid id)
        {
            var oven = GetById(id);
            if (oven != null)
            {
                _ovens.Remove(oven);
                return true;
            }

            return false;
        }

        private void SeedData()
        {
            _ovens = new List<Oven>
            {
                new Oven { Id = Guid.NewGuid(), Code = "Oven1", MaxTempInC = 250, Capacity = 20 },
                new Oven { Id = Guid.NewGuid(), Code = "Oven2", MaxTempInC = 300, Capacity = 25 },
                new Oven { Id = Guid.NewGuid(), Code = "Oven3", MaxTempInC = 350, Capacity = 30 }
            };
        }
    }
}
