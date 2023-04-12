using Microsoft.EntityFrameworkCore;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAP_API.DataAccess.Repositories
{
    public class StockedProductRepository : IStockedProductRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<StockedProduct> _stockedProducts;

        public StockedProductRepository(DbContext context)
        {
            this._context = context;
            this._stockedProducts = context.Set<StockedProduct>();
        }

        public StockedProduct Create(StockedProduct entity)
        {
            _stockedProducts.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public bool Delete(Guid id)
        {
            var stockedProduct = GetById(id);
            if (stockedProduct != null)
            {
                _stockedProducts.Remove(stockedProduct);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public IEnumerable<StockedProduct> GetAll()
        {
            return _stockedProducts;
        }

        public StockedProduct GetById(Guid id)
        {
            return _stockedProducts.FirstOrDefault(x => x.Id == id);
        }

        public List<StockedProduct> GetByProductId(Guid productId)
        {
            return _stockedProducts.Where(sp => sp.Product.Id == productId).ToList();
        }

        public StockedProduct Update(StockedProduct entity)
        {
            StockedProduct stockedProduct = _stockedProducts.FirstOrDefault(x => x.Id == entity.Id);
            if (stockedProduct == null)
            {
                throw new Exception("Stocked product not found");
            }
            _stockedProducts.Remove(stockedProduct);
            _stockedProducts.Add(entity);
            _context.SaveChanges();


            return entity;
        }

        public StockedProduct GetByLocationAndProduct(Guid locationId, Guid productId)
        {
            return _stockedProducts.FirstOrDefault(x => x.Location.Id == locationId && x.Product.Id == productId);
        }
    }
}
