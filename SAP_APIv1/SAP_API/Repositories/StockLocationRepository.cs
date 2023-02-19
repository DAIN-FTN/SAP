using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.Repositories
{
    public class StockLocationRepository : IStockLocationRepository
    {
        private readonly List<StockLocation> _stockLocations = new List<StockLocation>();

        public StockLocationRepository()
        {
            SeedData();
        }

        private void SeedData()
        {
            _stockLocations.Add(new StockLocation
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                Code = "L1",
                Capacity = 200
            });
            _stockLocations.Add(new StockLocation
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                Code = "L2",
                Capacity = 100
            });
           
        }

        public StockLocation Create(StockLocation entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockLocation> GetAll()
        {
            throw new NotImplementedException();
        }

        public StockLocation GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public StockLocation Update(StockLocation entity)
        {
            throw new NotImplementedException();
        }
    }
}
