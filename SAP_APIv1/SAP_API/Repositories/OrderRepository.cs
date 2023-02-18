using SAP_API.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace SAP_API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public OrderRepository() { 
            SeedData(); 
        }

        public IEnumerable<Order> GetAll()
        {
            return _orders;
        }

        public Order GetById(Guid id)
        {
            return _orders.SingleOrDefault(o => o.Id == id);
        }

        public Order Create(Order entity)
        {
            entity.Id = Guid.NewGuid();
            _orders.Add(entity);
            return entity;
        }

        public Order Update(Order entity)
        {
            var order = _orders.SingleOrDefault(o => o.Id == entity.Id);
            if (order != null)
            {
                order.ShouldBeDoneAt = entity.ShouldBeDoneAt;
                order.Status = entity.Status;
                order.Customer = entity.Customer;
            }
            return order;
        }

        public bool Delete(Guid id)
        {
            var order = _orders.SingleOrDefault(o => o.Id == id);
            if (order != null)
            {
                _orders.Remove(order);
                return true;
            }
            return false;
        }

        private void SeedData()
        {
            _orders.Add(new Order
            {
                Id = Guid.NewGuid(),
                ShouldBeDoneAt = DateTime.Now,
                Status = OrderStatus.Created,
                Customer = new Customer { Email = "jd@gmail.com", FullName = "Johm", Telephone = "+381691025544" }
            });
            _orders.Add(new Order
            {
                Id = Guid.NewGuid(),
                ShouldBeDoneAt = DateTime.Now.AddDays(1),
                Status = OrderStatus.Cancelled,
                Customer = new Customer { Email = "jd@gmail.com", FullName = "Johm", Telephone = "+381691025544" }
            });
            _orders.Add(new Order
            {
                Id = Guid.NewGuid(),
                ShouldBeDoneAt = DateTime.Now.AddDays(2),
                Status = OrderStatus.Cancelled,
                Customer = new Customer { Email = "jd@gmail.com", FullName = "Johm", Telephone = "+381691025544" }
            });
            _orders.Add(new Order
            {
                Id = Guid.NewGuid(),
                ShouldBeDoneAt = DateTime.Now.AddDays(3),
                Status = OrderStatus.Cancelled,
                Customer = new Customer { Email = "jd@gmail.com", FullName = "Johm", Telephone = "+381691025544" }
            });
            _orders.Add(new Order
            {
                Id = Guid.NewGuid(),
                ShouldBeDoneAt = DateTime.Now.AddDays(4),
                Status = OrderStatus.Confirmed,
                Customer = new Customer { Email = "jd@gmail.com", FullName = "Johm", Telephone = "+381691025544" }
            });
        }
    }
}
