using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAP_API.Repositories
{
    public class StockedProductRepository : IStockedProductRepository
    {
        private List<StockedProduct> _stockedProducts = new List<StockedProduct>();

        public StockedProductRepository()
        {
            SeedData();
        }


        public StockedProduct Create(StockedProduct entity)
        {
            _stockedProducts.Add(entity);
            return entity;
        }

        public bool Delete(Guid id)
        {
            return _stockedProducts.Remove(GetById(id));
        }

        public IEnumerable<StockedProduct> GetAll()
        {
            return _stockedProducts;
        }

        public StockedProduct GetById(Guid id)
        {
            return _stockedProducts.FirstOrDefault(x => x.Id == id);
        }

        public List<StockedProduct> GetByProductId(Guid productId)
        {
            return _stockedProducts.FindAll(sp => sp.Product.Id == productId);
        }

        public StockedProduct Update(StockedProduct entity)
        {
            throw new NotImplementedException();
        }

        public StockedProduct GetByLocationAndProduct(Guid locationId, Guid productId)
        {
            return _stockedProducts.FirstOrDefault(x => x.Location.Id == locationId && x.Product.Id == productId);
        }

        private void SeedData()
        {
            _stockedProducts = new List<StockedProduct>
            {
                new StockedProduct
                {
                    Id = Guid.NewGuid(),
                    Quantity = 20,
                    ReservedQuantity = 10,
                    Product = new Product
                    {
                        BakingTempInC = 120,
                        BakingTimeInMins= 30,
                        Id= Guid.Parse("5cd54cb6-0df4-420f-96fd-f6e2cf6e2000"),
                        Name = "Chocolate Croissant",
                        Size = 4
                    },
                    Location = new StockLocation
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        Code = "L2",
                        Capacity = 100
                    }
                },
                new StockedProduct
                {
                    Id = Guid.NewGuid(),
                    Quantity = 20,
                    ReservedQuantity = 10,
                    Product = new Product
                    {
                        BakingTempInC = 120,
                        BakingTimeInMins= 30,
                        Id= Guid.Parse("5cd54cb6-0df4-420f-96fd-f6e2cf6e2001"),
                        Name = "Vanilla Croissant",
                        Size = 4
                    },
                    Location = new StockLocation
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        Code = "L2",
                        Capacity = 100
                    }
                },
                new StockedProduct
                {
                    Id = Guid.NewGuid(),
                    Quantity = 300,
                    ReservedQuantity = 10,
                    Product = new Product
                    {
                        BakingTempInC = 120,
                        BakingTimeInMins= 30,
                        Id= Guid.Parse("5cd54cb6-0df4-420f-96fd-f6e2cf6e2000"),
                        Name = "Chocolate Croissant",
                        Size = 4
                    },
                    Location = new StockLocation
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Code = "L1",
                        Capacity = 200
                    }
                },
                new StockedProduct
                {
                    Id = Guid.NewGuid(),
                    Quantity = 20,
                    ReservedQuantity = 10,
                    Product = new Product
                    {
                        BakingTempInC = 150,
                        BakingTimeInMins= 60,
                        Id= Guid.Parse("d174996a-63e4-4b6b-b322-fdf235d91444"),
                        Name = "Pizza",
                        Size = 6
                    },
                    Location = new StockLocation
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Code = "L1",
                        Capacity = 200
                    }
                },                
                new StockedProduct
                {
                    Id = Guid.NewGuid(),
                    Quantity = 200,
                    ReservedQuantity = 10,
                    Product = new Product
                    {
                        BakingTempInC = 150,
                        BakingTimeInMins= 60,
                        Id= Guid.Parse("d174996a-63e4-4b6b-b322-fdf235d91444"),
                        Name = "Pizza",
                        Size = 6
                    },
                    Location = new StockLocation
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        Code = "L2",
                        Capacity = 100
                    }
                },
                new StockedProduct
                {
                    Id = Guid.NewGuid(),
                    Quantity = 20,
                    ReservedQuantity = 10,
                    Product = new Product
                    {
                         BakingTempInC = 200,
                         BakingTimeInMins= 45,
                         Id= Guid.Parse("725b7a84-c8de-4c77-9c61-dcdafe0ea091"),
                         Name = "Bagguete",
                         Size = 6
                    },
                    Location = new StockLocation
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        Code = "L2",
                        Capacity = 100
                    }
                }
            };
        }
    }
}
