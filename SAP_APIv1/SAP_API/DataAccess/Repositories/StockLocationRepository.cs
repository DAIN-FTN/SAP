using Microsoft.EntityFrameworkCore;
using SAP_API.Models;
using System;
using System.Collections.Generic;

namespace SAP_API.DataAccess.Repositories
{
    public class StockLocationRepository : IStockLocationRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<StockLocation> _stockLocations;

        public StockLocationRepository(DbContext context)
        {
            _context = context;
            _stockLocations = context.Set<StockLocation>();
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
