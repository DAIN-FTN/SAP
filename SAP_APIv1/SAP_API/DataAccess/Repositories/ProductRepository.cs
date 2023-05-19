using Microsoft.EntityFrameworkCore;
using SAP_API.DataAccess.DbContexts;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAP_API.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BakeryContext _context;
        private readonly DbSet<Product> _products;

        public ProductRepository(BakeryContext context)
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
            return _products
                .ToList();
        }

        public Product GetById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetByName(string name)
        {
            return _products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public Product Update(Product updatedProduct)
        {
            Product product = GetById(updatedProduct.Id);
            if(product != null)
            {
                _products.Update(updatedProduct);
                _context.SaveChanges();

                return updatedProduct;
            }
            throw new Exception("Product not found");
        }

    }
}
