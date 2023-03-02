using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DataAccess.Repositories
{
    public class ProductToPrepareRepository : IProductToPrepareRepository
    {
        private readonly List<ProductToPrepare> _productsToPrepare = new List<ProductToPrepare>();
        public ProductToPrepare Create(ProductToPrepare entity)
        {
            entity.Id = Guid.NewGuid();
            _productsToPrepare.Add(entity);
            return entity;
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductToPrepare> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ProductToPrepare> GetByBakingProgramId(Guid bakingProgramId)
        {
            return _productsToPrepare.FindAll(x => x.BakingProgram.Id == bakingProgramId);
        }

        public ProductToPrepare GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductToPrepare Update(ProductToPrepare entity)
        {
            throw new NotImplementedException();
        }
    }
}
