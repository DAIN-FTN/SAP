using SAP_API.DTOs;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Services
{
    public interface IOrderService
    {
        public Order CreateOrder(DateTime shouldBeDoneAt, Customer customer, List<OrderProduct> orderProducts );
    }
}
