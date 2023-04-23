using Microsoft.EntityFrameworkCore;
using SAP_API.DataAccess.DbContexts;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAP_API.DataAccess.Repositories
{
    public class StockLocationRepository : IStockLocationRepository
    {
        private readonly BakeryContext _context;
        private readonly DbSet<StockLocation> _stockLocations;

        public StockLocationRepository(BakeryContext context)
        {
            _context = context;
            _stockLocations = context.Set<StockLocation>();
        }

        public StockLocation Create(StockLocation entity)
        {
            StockLocation stockLocation = _stockLocations.Add(entity).Entity;
            _context.SaveChanges();

            return stockLocation;
        }

        public bool Delete(Guid id)
        {
            StockLocation stockLocation = GetById(id);
            if (stockLocation != null)
            {
                _stockLocations.Remove(stockLocation);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<StockLocation> GetAll()
        {
            return _stockLocations.ToList();
        }

        public StockLocation GetById(Guid id)
        {
            return _stockLocations.SingleOrDefault(sl => sl.Id == id);
        }

        public StockLocation Update(StockLocation updatedStockLocation)
        {
            StockLocation stockLocation = GetById(updatedStockLocation.Id);
            if (stockLocation != null)
            {
                _stockLocations.Remove(stockLocation);
                _context.SaveChanges();
                _stockLocations.Add(updatedStockLocation);
                _context.SaveChanges();

                return stockLocation;
            }
            throw new Exception("StockLocation not found");
        }
    }
}
