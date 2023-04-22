using Microsoft.EntityFrameworkCore;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SAP_API.DataAccess.DbContexts;


namespace SAP_API.DataAccess.Repositories
{
    public class StockedProductRepository : IStockedProductRepository
    {
        private readonly BakeryContext _context;
        private readonly DbSet<StockedProduct> _stockedProducts;

        public StockedProductRepository(BakeryContext context)
        {
            this._context = context;
            this._stockedProducts = context.Set<StockedProduct>();
        }

        public StockedProduct Create(StockedProduct entity)
        {
            _stockedProducts.Add(entity);
            _context.SaveChanges();

            return GetById(entity.Id);
        }

        public bool Delete(Guid id)
        {
            StockedProduct stockedProduct = GetById(id);
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
            return _stockedProducts
                .Include(x => x.Location)
                .Include(x => x.Product)
                .ToList();
        }

        public StockedProduct GetById(Guid id)
        {
            return _stockedProducts
                 .Include(x => x.Location)
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<StockedProduct> GetByProductId(Guid productId)
        {
            return _stockedProducts
                .Include(x => x.Location)
                .Include(x => x.Product)
                .Where(sp => sp.Product.Id == productId).ToList();
        }

        public StockedProduct Update(StockedProduct updatedStockProduct)
        {
            StockedProduct stockedProduct = _stockedProducts.FirstOrDefault(x => x.Id == updatedStockProduct.Id);
            if (stockedProduct != null)
            {
                _stockedProducts.Remove(stockedProduct);
                _stockedProducts.Add(updatedStockProduct);
                _context.SaveChanges();

                return updatedStockProduct;
            }
            throw new Exception("Stocked product not found");
        }

        public StockedProduct GetByLocationAndProduct(Guid locationId, Guid productId)
        {
            return _stockedProducts
                .Include(x => x.Location)
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Location.Id == locationId && x.Product.Id == productId);
        }
    }
}
