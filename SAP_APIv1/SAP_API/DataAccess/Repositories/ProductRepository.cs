using Microsoft.EntityFrameworkCore;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Product> _products;

        public ProductRepository(DbContext context)
        {
            this._context = context;
            this._products = context.Set<Product>();
        }
        public Product Create(Product entity)
        {
            _products.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public bool Delete(Guid id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetByName(string name)
        {
            return _products.Where(p => p.Name.Contains(name)).ToList();
        }

        public Product Update(Product productUpdate)
        {
            Product product = GetById(productUpdate.Id);

            product.BakingTimeInMins = productUpdate.BakingTimeInMins;
            product.Name = productUpdate.Name;
            product.BakingTempInC = productUpdate.BakingTempInC;
            product.Size = productUpdate.Size;

            _products.Update(product);
            _context.SaveChanges();

            return product;
        }

    }
}
