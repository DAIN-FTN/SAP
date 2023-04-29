using Microsoft.EntityFrameworkCore;
using SAP_API.Models;
using System;
using SAP_API.DataAccess.DbContexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DataAccess.Repositories
{
    public class ProductToPrepareRepository : IProductToPrepareRepository
    {
        private readonly BakeryContext _context;
        private readonly DbSet<ProductToPrepare> _productsToPrepare;

        public ProductToPrepareRepository(BakeryContext context)
        {
            this._context = context;
            this._productsToPrepare = context.Set<ProductToPrepare>();
        }

        public ProductToPrepare Create(ProductToPrepare entity)
        {
            entity.Id = Guid.NewGuid();
            _productsToPrepare.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(Guid id)
        {
            ProductToPrepare productToPrepare = GetById(id);
            if (productToPrepare != null)
            {
                _productsToPrepare.Remove(productToPrepare);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<ProductToPrepare> GetAll()
        {
            return _productsToPrepare
                .Include(x => x.BakingProgram)
                .Include(x => x.Product)
                .Include(x => x.Order)
                .ToList();
        }

        public List<ProductToPrepare> GetByBakingProgramId(Guid bakingProgramId)
        {
            return _productsToPrepare
                .Include(x => x.BakingProgram)
                .Include(x => x.Product)
                .Include(x => x.Order)
                .Where(x => x.BakingProgram.Id == bakingProgramId).ToList();
        }

        public ProductToPrepare GetById(Guid id)
        {
            return _productsToPrepare
                .Include(x => x.BakingProgram)
                .Include(x => x.Product)
                .Include(x => x.Order)
                .FirstOrDefault(x => x.Id == id);
        }

        public ProductToPrepare Update(ProductToPrepare entity)
        {
            ProductToPrepare productToPrepare = GetById(entity.Id);
            if (productToPrepare != null)
            {
                Delete(entity.Id);
                Create(entity);
                _context.SaveChanges();
            }
            throw new Exception("Product to prepare not found");
        }
    } 
}
