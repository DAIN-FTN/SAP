﻿using Microsoft.EntityFrameworkCore;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DataAccess.Repositories
{
    public class ReservedOrderProductRepository : IReservedOrderProductRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<ReservedOrderProduct> _reservedOrderProducts;

        public ReservedOrderProductRepository(DbContext context)
        {
            this._context = context;
            this._reservedOrderProducts = context.Set<ReservedOrderProduct>();
        }

        public List<ReservedOrderProduct> GetByOrderIdAndProductId(Guid orderId, Guid productId)
        {
            return _reservedOrderProducts.Where(x => x.Order.Id == orderId && x.Product.Id == productId).ToList();
        }
    }
}
