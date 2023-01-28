using SAP_API.Models;

namespace SAP_API.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private List<Product> _products = new List<Product>();

        public ProductRepository()
        {
            SeedData();
        }

        public Product Create(Product entity)
        {
            _products.Add(entity);
            return entity;
        }

        public bool Delete(Guid id)
        {
            return _products.Remove(_products.FirstOrDefault(p => p.Id == id));
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product GetById (Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetByName(string name) 
        { 
            return _products.FindAll(p => p.Name == name);
        }

        public Product Update(Product productUpdate)
        {
            Product product = GetById(productUpdate.Id);

            product.BakingTimeInMins = productUpdate.BakingTimeInMins; 
            product.Name = productUpdate.Name; 
            product.BakingTempInC = productUpdate.BakingTempInC;
            product.Size = productUpdate.Size;

            return product;
        }

        private List<Product> SeedData()
        {
            return new List<Product>()
            {
                new Product
                {
                    BakingTempInC = 120,
                    BakingTimeInMins= 30,
                    Id= Guid.NewGuid(),
                    Name = "Croissant",
                    Size = 4
                },
                new Product
                {
                    BakingTempInC = 150,
                    BakingTimeInMins= 60,
                    Id= Guid.NewGuid(),
                    Name = "Pizza",
                    Size = 6
                },
                new Product
                {
                    BakingTempInC = 200,
                    BakingTimeInMins= 45,
                    Id= Guid.NewGuid(),
                    Name = "Bagguete",
                    Size = 6
                }
            };
        }
    }
}
