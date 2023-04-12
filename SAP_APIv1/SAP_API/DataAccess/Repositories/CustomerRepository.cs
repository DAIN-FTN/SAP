using Microsoft.EntityFrameworkCore;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAP_API.DataAccess.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly DbContext _context;
        private readonly DbSet<Customer> _dbSet;

        public CustomerRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<Customer>();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _dbSet.ToList();
        }

        public Customer GetById(Guid id)
        {
            return _dbSet.FirstOrDefault(c => c.Id == id);
        }

        public Customer Create(Customer Customer)
        {
            _dbSet.Add(Customer);
            _context.SaveChanges();
            return Customer;
        }

        public Customer Update(Customer Customer)
        {
            _dbSet.Update(Customer);
            _context.SaveChanges();
            return Customer;
        }

        public bool Delete(Guid id)
        {
            var Customer = GetById(id);
            if (Customer != null)
            {
                _dbSet.Remove(Customer);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
