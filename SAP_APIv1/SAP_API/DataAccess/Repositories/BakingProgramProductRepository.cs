using Microsoft.EntityFrameworkCore;
using SAP_API.DataAccess.DbContexts;
using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DataAccess.Repositories
{
    public class BakingProgramProductRepository : IBakingProgramProductRepository
    {

        private readonly BakeryContext _context;
        private readonly DbSet<BakingProgramProduct> _bakingProgramProducts;

        public BakingProgramProductRepository(BakeryContext context)
        {
            this._context = context;
            this._bakingProgramProducts = context.Set<BakingProgramProduct>();
        }

        public BakingProgramProduct Create(BakingProgramProduct entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BakingProgramProduct> GetAll()
        {
            throw new NotImplementedException();
        }

        public BakingProgramProduct GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<BakingProgramProduct> GetByOrderId(Guid orderId)
        {
            return _bakingProgramProducts
              .Include(bakingProgramProduct => bakingProgramProduct.BakingProgram.Oven)
              .Include(bakingProgramProduct => bakingProgramProduct.Product)
              .Where(x => x.OrderId.Equals(orderId)).ToList();
        }

        public BakingProgramProduct Update(BakingProgramProduct entity)
        {
            throw new NotImplementedException();
        }
    }
}
