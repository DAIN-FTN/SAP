using SAP_API.Models;

namespace SAP_API.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private List<Product> _products;

        public ProductRepository(List<Product> products)
        {
            _products = products;
        }

        public Product create(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> getAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> getBy(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product update(Product entity)
        {
            throw new NotImplementedException();
        }

        private List<Product> SeedData()
        {
            return new List<Product>()
            {
           
            };
        }
    }
}
