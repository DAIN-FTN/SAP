using SAP_API.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SAP_API.DataAccess.DbContexts;

namespace SAP_API.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BakeryContext _context;
        private readonly DbSet<Order> _orders;

        public OrderRepository(BakeryContext context)
        {
            this._context = context;
            this._orders = context.Set<Order>();
        }


        public IEnumerable<Order> GetAll()
        {
            return _orders
                .Include(order => order.Products)
                .ToList();
        }

        public Order GetById(Guid id)
        {
            return _orders.Include(o => o.Products)
                .ThenInclude(prod => prod.Product)
                .SingleOrDefault(o => o.Id == id);
        }

        public Order Create(Order entity)
        {
            entity.Id = Guid.NewGuid();
            _orders.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public Order Update(Order entity)
        {
            Order order = _orders.SingleOrDefault(o => o.Id == entity.Id);
            if (order != null)
            {
                _context.Remove(order);
                _context.Add(entity);
                _context.SaveChanges();
            }
            throw new Exception("Order not found");
        }

        public bool Delete(Guid id)
        {
            var order = _orders.SingleOrDefault(o => o.Id == id);
            if (order != null)
            {
                _orders.Remove(order);
                _context.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
