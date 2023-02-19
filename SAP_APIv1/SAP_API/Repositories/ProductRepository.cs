using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.Repositories
{
    public class ProductRepository : IProductRepository
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

        public Product GetById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetByName(string name)
        {
            return _products.FindAll(p => p.Name.Contains(name));
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

        private void SeedData()
        {
            _products = new List<Product>
            {
                new Product
                {
                    BakingTempInC = 120,
                    BakingTimeInMins= 30,
                    Id= Guid.Parse("5cd54cb6-0df4-420f-96fd-f6e2cf6e2000"),
                    Name = "Chocolate Croissant",
                    Size = 2
                },
                new Product
                {
                    BakingTempInC = 140,
                    BakingTimeInMins= 30,
                    Id= Guid.Parse("5cd54cb6-0df4-420f-96fd-f6e2cf6e2001"),
                    Name = "Vanilla Croissant",
                    Size = 1
                },
                new Product
                {
                    BakingTempInC = 120,
                    BakingTimeInMins= 30,
                    Id= Guid.Parse("d174996a-63e4-4b6b-b322-fdf235d91444"),
                    Name = "Pizza",
                    Size = 6
                },
                new Product
                {
                    BakingTempInC = 200,
                    BakingTimeInMins= 45,
                    Id= Guid.Parse("725b7a84-c8de-4c77-9c61-dcdafe0ea091"),
                    Name = "Bagguete",
                    Size = 6
                }
            };
        }
    }
}
