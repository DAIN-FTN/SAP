using SAP_API.DataAccess.DbContexts;
using SAP_API.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SAP_API.DataAccess
{
    public class DataSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            SeedOrders(modelBuilder.Entity<Order>());
            SeedOvens(modelBuilder.Entity<Oven>());
            SeedBakingPrograms(modelBuilder.Entity<BakingProgram>());
            SeedProducts(modelBuilder.Entity<Product>());
            SeedReservedOrderProducts(modelBuilder.Entity<ReservedOrderProduct>());
            SeedStockedProducts(modelBuilder.Entity<StockedProduct>());
            SeedStockLocations(modelBuilder.Entity<StockLocation>());
            SeedRoles(modelBuilder.Entity<Role>());
            SeedUsers(modelBuilder.Entity<User>());
            SeedBakingProgramProducts(modelBuilder.Entity<BakingProgramProduct>());
        }

        private static void SeedRoles(EntityTypeBuilder<Role> entityTypeBuilder)
        {
            entityTypeBuilder.HasData(
                new Role
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "Admin",
                },
                new Role
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = "Staff",
                });
        }

        private static void SeedBakingProgramProducts(EntityTypeBuilder<BakingProgramProduct> bakingProgramProducts)
        {
            bakingProgramProducts.HasData(
                    new BakingProgramProduct
                    {
                        Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        OrderId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        ProductId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        BakingProgramId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        QuantityТoBake = 5,
                    },
                    new BakingProgramProduct
                    {
                        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                        OrderId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        ProductId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        BakingProgramId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        QuantityТoBake = 5,
                    },
                    new BakingProgramProduct
                    {
                        Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                        OrderId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        ProductId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        BakingProgramId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                        QuantityТoBake = 5,
                        
                    },
                    new BakingProgramProduct
                    {
                        Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                        ProductId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                        OrderId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        BakingProgramId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                        QuantityТoBake = 5,
                    },
                    new BakingProgramProduct
                    {
                        Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                        ProductId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                        OrderId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                        BakingProgramId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                        QuantityТoBake = 5,
                    }
                );
        }

        private static void SeedOrders(EntityTypeBuilder<Order> orders)
        {
            orders.HasData(
                new Order
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    ShouldBeDoneAt = DateTime.Now,
                    Status = OrderStatus.Created,
                    CustomerFullName = "Jane Smith",
                    CustomerEmail = "janesmith@example.com",
                    CustomerTelephone = "+44 20 5555 5555"
                },
               new Order
               {
                   Id = new Guid("00000000-0000-0000-0000-000000000001"),
                   ShouldBeDoneAt = DateTime.Now.AddDays(1),
                   Status = OrderStatus.Cancelled,
                   CustomerFullName = "Jane Smith",
                   CustomerEmail = "janesmith@example.com",
                   CustomerTelephone = "+44 20 5555 5555"
               },
               new Order
               {
                   Id = new Guid("00000000-0000-0000-0000-000000000002"),
                   ShouldBeDoneAt = DateTime.Now.AddDays(2),
                   Status = OrderStatus.Cancelled,
                   CustomerFullName = "Jane Smith",
                   CustomerEmail = "janesmith@example.com",
                   CustomerTelephone = "+44 20 5555 5555"
               }
            );
        }

        private static void SeedBakingPrograms(EntityTypeBuilder<BakingProgram> bakingPrograms)
        {
            bakingPrograms.HasData(
                new BakingProgram
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    PreparedByUserId = null,
                    OvenId = new Guid("00000000-0000-0000-0000-000000000008"),
                    Code = "Code1",
                    CreatedAt = new DateTime(2020, 1, 1, 12, 0, 0),
                    Status = BakingProgramStatus.Created,
                    BakingTimeInMins = 30,
                    BakingTempInC = 120,
                    BakingProgrammedAt = DateTime.Now,
                    BakingStartedAt = null,
                    BakingEndsAt = null,
                    RemainingOvenCapacity = 10,
                    
                },
                new BakingProgram
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    PreparedByUserId = null,
                    OvenId = new Guid("00000000-0000-0000-0000-000000000001"),
                    Code = "Code2",
                    CreatedAt = new DateTime(2020, 2, 1, 12, 0, 0),
                    Status = BakingProgramStatus.Created,
                    BakingTimeInMins = 30,
                    BakingTempInC = 140,
                    BakingProgrammedAt = DateTime.Now,
                    BakingStartedAt = null,
                    BakingEndsAt = null,
                    RemainingOvenCapacity = 10
                },
                new BakingProgram
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    PreparedByUserId = new Guid("00000000-0000-0000-0000-000000000002"),
                    OvenId = new Guid("00000000-0000-0000-0000-000000000002"),
                    Code = "Code3",
                    CreatedAt = new DateTime(2020, 3, 1, 12, 0, 0),
                    Status = BakingProgramStatus.Done,
                    BakingTimeInMins = 12,
                    BakingTempInC = 190,
                    BakingProgrammedAt = DateTime.Now,
                    BakingStartedAt = new DateTime(2023, 12, 2, 11, 5, 0),
                    BakingEndsAt = new DateTime(2023, 12, 2, 11, 17, 0),
                    RemainingOvenCapacity = 10,
                }
            );

        }

        private static void SeedOvens(EntityTypeBuilder<Oven> ovens)
        {
            ovens.HasData(
                new Oven { Id = new Guid("00000000-0000-0000-0000-000000000008"), Code = "Oven1", MaxTempInC = 250, Capacity = 20 },
                new Oven { Id = new Guid("00000000-0000-0000-0000-000000000001"), Code = "Oven2", MaxTempInC = 300, Capacity = 25 },
                new Oven { Id = new Guid("00000000-0000-0000-0000-000000000002"), Code = "Oven3", MaxTempInC = 350, Capacity = 30 }
                );
        }

        private static void SeedProducts(EntityTypeBuilder<Product> products)
        {
            products.HasData(
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    BakingTempInC = 120,
                    BakingTimeInMins = 30,
                    Name = "Chocolate Croissant",
                    Size = 2
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    BakingTempInC = 140,
                    BakingTimeInMins = 30,
                    Name = "Vanilla Croissant",
                    Size = 1
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    BakingTempInC = 120,
                    BakingTimeInMins = 30,
                    Name = "Pizza",
                    Size = 6
                },
                new Product
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    BakingTempInC = 200,
                    BakingTimeInMins = 45,
                    Name = "Bagguete",
                    Size = 6
                }
             );
        }

        private static void SeedReservedOrderProducts(EntityTypeBuilder<ReservedOrderProduct> reservedOrderProducts)
        {
            reservedOrderProducts.HasData(
                new ReservedOrderProduct
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    OrderId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    StockLocationId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ReservedQuantity = 5,
                    PreparedQuantity = 0,
                }, new ReservedOrderProduct
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    OrderId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    StockLocationId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ReservedQuantity = 5,
                    PreparedQuantity = 0,
                },
                new ReservedOrderProduct
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    OrderId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    StockLocationId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ReservedQuantity = 5,
                    PreparedQuantity = 0,
                },
                new ReservedOrderProduct
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    OrderId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    StockLocationId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ReservedQuantity = 5,
                    PreparedQuantity = 0,

                },
                new ReservedOrderProduct
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    OrderId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    StockLocationId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ReservedQuantity = 5,
                    PreparedQuantity = 0
                }
                );
        }

        private static void SeedStockedProducts(EntityTypeBuilder<StockedProduct> stockedProducts)
        {
            stockedProducts.HasData(
                new StockedProduct
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    Quantity = 20,
                    ReservedQuantity = 10,
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                },
                new StockedProduct
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Quantity = 20,
                    ReservedQuantity = 10,
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                },
                new StockedProduct
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Quantity = 300,
                    ReservedQuantity = 10,
                },
                new StockedProduct
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Quantity = 20,
                    ReservedQuantity = 10,
                },
                new StockedProduct
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    LocationId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Quantity = 20,
                    ReservedQuantity = 10
                }
                );
        }

        private static void SeedStockLocations(EntityTypeBuilder<StockLocation> stockLocations)
        {
            stockLocations.HasData(
                new StockLocation
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Code = "L1",
                    Capacity = 200
                },
                new StockLocation
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Code = "L2",
                    Capacity = 100
                }
            );
        }

        private static void SeedUsers(EntityTypeBuilder<User> users)
        {
            users.HasData(
                   
                   new User
                   {
                       Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                       Username = "AleksandarAdmin",
                       Password = "10.Dq24kqmfYfyJ/ZM90uQt3A==.VRQEd9C+pfkWA/sHxLZO9+wEYVMWYMww0MZZIy0nEkQ=",
                       RoleId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                       Active = true
                   },
                    new User
                    {
                        Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                        Username = "AleksandarStaff",
                        Password = "10.Dq24kqmfYfyJ/ZM90uQt3A==.VRQEd9C+pfkWA/sHxLZO9+wEYVMWYMww0MZZIy0nEkQ=",
                        RoleId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                        Active = true
                    }
                );
        }
    }

   }
