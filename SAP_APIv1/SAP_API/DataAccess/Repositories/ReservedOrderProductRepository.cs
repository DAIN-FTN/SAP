using SAP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP_API.DataAccess.Repositories
{
    public class ReservedOrderProductRepository : IReservedOrderProductRepository
    {
        private List<ReservedOrderProduct> _orderProducts = new List<ReservedOrderProduct>();

        public ReservedOrderProductRepository()
        {
            SeedData();
        }

        private void SeedData()
        {
            _orderProducts = new List<ReservedOrderProduct>
            {
                new ReservedOrderProduct
                {
                    Id = Guid.NewGuid(),
                    Product = new Product
                    {
                        BakingTempInC = 120,
                        BakingTimeInMins = 30,
                        Id = Guid.Parse("5cd54cb6-0df4-420f-96fd-f6e2cf6e2000"),
                        Name = "Chocolate Croissant",
                        Size = 2
                    },
                    Order = new Order
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000000"),
                        ShouldBeDoneAt = new DateTime(2023, 2, 4, 12, 0, 0),
                        Status = OrderStatus.Confirmed,
                        Customer = new Customer()
                    },
                    ReservedQuantity = 5,
                    PreparedQuantity = 0,
                    LocationWhereProductIsReserved = new StockLocation
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        Code = "L2",
                        Capacity = 100
                    }
                }, new ReservedOrderProduct
                {
                    Id = Guid.NewGuid(),
                    Product = new Product
                    {
                        BakingTempInC = 120,
                        BakingTimeInMins = 30,
                        Id = Guid.Parse("5cd54cb6-0df4-420f-96fd-f6e2cf6e2000"),
                        Name = "Chocolate Croissant",
                        Size = 2
                    },
                    Order = new Order
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000000"),
                        ShouldBeDoneAt = new DateTime(2023, 2, 4, 12, 0, 0),
                        Status = OrderStatus.Confirmed,
                        Customer = new Customer()
                    },
                    ReservedQuantity = 5,
                    PreparedQuantity = 0,
                    LocationWhereProductIsReserved = new StockLocation
                    {
                        Id =new Guid("00000000-0000-0000-0000-000000000001"),
                        Code = "L1",
                        Capacity = 200
                    }
                },
                new ReservedOrderProduct
                {
                    Id = Guid.NewGuid(),
                    Product = new Product
                    {
                        BakingTempInC = 120,
                        BakingTimeInMins = 30,
                        Id = Guid.Parse("5cd54cb6-0df4-420f-96fd-f6e2cf6e2000"),
                        Name = "Chocolate Croissant",
                        Size = 2
                    },
                    Order = new Order
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000000"),
                        ShouldBeDoneAt = new DateTime(2023, 2, 4, 12, 0, 0),
                        Status = OrderStatus.Confirmed,
                        Customer = new Customer()
                    },
                    ReservedQuantity = 5,
                    PreparedQuantity = 0,
                    LocationWhereProductIsReserved = new StockLocation
                    {
                        Id =new Guid("00000000-0000-0000-0000-000000000001"),
                        Code = "L1",
                        Capacity = 200
                    }
                },
                new ReservedOrderProduct
                {
                    Id = Guid.NewGuid(),
                    Product =  new Product
                    {
                        BakingTempInC = 120,
                        BakingTimeInMins= 30,
                        Id= Guid.Parse("d174996a-63e4-4b6b-b322-fdf235d91444"),
                        Name = "Pizza",
                        Size = 6
                    },
                    Order = new Order
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000000"),
                        ShouldBeDoneAt = new DateTime(2023, 2, 4, 12, 0, 0),
                        Status = OrderStatus.Confirmed,
                        Customer = new Customer()
                    },
                    ReservedQuantity = 5,
                    PreparedQuantity = 0,
                    LocationWhereProductIsReserved = new StockLocation
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002"),
                        Code = "L2",
                        Capacity = 100
                    }
                },
                new ReservedOrderProduct
                {
                    Id = Guid.NewGuid(),
                    Product =  new Product
                    {
                        BakingTempInC = 120,
                        BakingTimeInMins= 30,
                        Id= Guid.Parse("d174996a-63e4-4b6b-b322-fdf235d91444"),
                        Name = "Pizza",
                        Size = 6
                    },
                    Order = new Order
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000000"),
                        ShouldBeDoneAt = new DateTime(2023, 2, 4, 12, 0, 0),
                        Status = OrderStatus.Confirmed,
                        Customer = new Customer()
                    },
                    ReservedQuantity = 5,
                    PreparedQuantity = 0,
                    LocationWhereProductIsReserved = new StockLocation
                    {
                        Id =new Guid("00000000-0000-0000-0000-000000000001"),
                        Code = "L1",
                        Capacity = 200
                    }
                }
            };
        }

        public List<ReservedOrderProduct> GetByOrderIdAndProductId(Guid orderId, Guid productId)
        {
            return _orderProducts.FindAll(x => x.Order.Id == orderId && x.Product.Id == productId);
        }
    }
}
