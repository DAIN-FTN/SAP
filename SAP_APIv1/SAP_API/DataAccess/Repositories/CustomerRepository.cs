using Microsoft.EntityFrameworkCore;
using SAP_API.DataAccess.DbContexts;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAP_API.DataAccess.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly BakeryContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(BakeryContext context)
        {
            _context = context;
            _customers = context.Set<Customer>();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers.ToList();
        }

        public Customer GetById(Guid id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer Create(Customer customer)
        {
            _customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer Update(Customer updatedCustomer)
        {
            Customer customer = GetById(updatedCustomer.Id);
            if (customer != null)
            {
                _context.Remove(customer);
                _context.Add(updatedCustomer);
                _context.SaveChanges();
            }
            throw new Exception("Customer not found");
        }

        public bool Delete(Guid id)
        {
            Customer Customer = GetById(id);
            if (Customer != null)
            {
                _customers.Remove(Customer);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
