﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SAP_API.DataAccess.DbContexts;

namespace SAP_API.Migrations
{
    [DbContext(typeof(BakeryContext))]
    partial class BakeryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("SAP_API.Models.BakingProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BakingEndsAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("BakingProgrammedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("BakingStartedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("BakingTempInC")
                        .HasColumnType("integer");

                    b.Property<int>("BakingTimeInMins")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("OvenId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PreparedByUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("RemainingOvenCapacity")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("Code");

                    b.HasIndex("OvenId");

                    b.HasIndex("PreparedByUserId");

                    b.ToTable("BakingProgram");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000008"),
                            BakingProgrammedAt = new DateTime(2023, 4, 26, 10, 34, 40, 538, DateTimeKind.Local).AddTicks(7787),
                            BakingTempInC = 120,
                            BakingTimeInMins = 30,
                            Code = "Code1",
                            CreatedAt = new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            OvenId = new Guid("00000000-0000-0000-0000-000000000008"),
                            RemainingOvenCapacity = 10,
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            BakingProgrammedAt = new DateTime(2023, 4, 26, 10, 34, 40, 538, DateTimeKind.Local).AddTicks(9406),
                            BakingTempInC = 140,
                            BakingTimeInMins = 30,
                            Code = "Code2",
                            CreatedAt = new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            OvenId = new Guid("00000000-0000-0000-0000-000000000001"),
                            RemainingOvenCapacity = 10,
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            BakingEndsAt = new DateTime(2023, 12, 2, 11, 17, 0, 0, DateTimeKind.Unspecified),
                            BakingProgrammedAt = new DateTime(2023, 4, 26, 10, 34, 40, 538, DateTimeKind.Local).AddTicks(9592),
                            BakingStartedAt = new DateTime(2023, 12, 2, 11, 5, 0, 0, DateTimeKind.Unspecified),
                            BakingTempInC = 190,
                            BakingTimeInMins = 12,
                            Code = "Code3",
                            CreatedAt = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            OvenId = new Guid("00000000-0000-0000-0000-000000000002"),
                            PreparedByUserId = new Guid("00000000-0000-0000-0000-000000000008"),
                            RemainingOvenCapacity = 10,
                            Status = 5
                        });
                });

            modelBuilder.Entity("SAP_API.Models.BakingProgramProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BakingProgramId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("QuantityТoBake")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BakingProgramId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("BakingProgramProduct");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000008"),
                            BakingProgramId = new Guid("00000000-0000-0000-0000-000000000008"),
                            OrderId = new Guid("00000000-0000-0000-0000-000000000008"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000008"),
                            QuantityТoBake = 5
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            BakingProgramId = new Guid("00000000-0000-0000-0000-000000000008"),
                            OrderId = new Guid("00000000-0000-0000-0000-000000000008"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000008"),
                            QuantityТoBake = 5
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            BakingProgramId = new Guid("00000000-0000-0000-0000-000000000001"),
                            OrderId = new Guid("00000000-0000-0000-0000-000000000008"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000008"),
                            QuantityТoBake = 5
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            BakingProgramId = new Guid("00000000-0000-0000-0000-000000000001"),
                            OrderId = new Guid("00000000-0000-0000-0000-000000000008"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000002"),
                            QuantityТoBake = 5
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            BakingProgramId = new Guid("00000000-0000-0000-0000-000000000001"),
                            OrderId = new Guid("00000000-0000-0000-0000-000000000008"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000002"),
                            QuantityТoBake = 5
                        });
                });

            modelBuilder.Entity("SAP_API.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("text");

                    b.Property<string>("CustomerFullName")
                        .HasColumnType("text");

                    b.Property<string>("CustomerTelephone")
                        .HasColumnType("text");

                    b.Property<DateTime>("ShouldBeDoneAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000008"),
                            CustomerEmail = "janesmith@example.com",
                            CustomerFullName = "Jane Smith",
                            CustomerTelephone = "+44 20 5555 5555",
                            ShouldBeDoneAt = new DateTime(2023, 4, 26, 10, 34, 40, 526, DateTimeKind.Local).AddTicks(1432),
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            CustomerEmail = "janesmith@example.com",
                            CustomerFullName = "Jane Smith",
                            CustomerTelephone = "+44 20 5555 5555",
                            ShouldBeDoneAt = new DateTime(2023, 4, 27, 10, 34, 40, 534, DateTimeKind.Local).AddTicks(5862),
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            CustomerEmail = "janesmith@example.com",
                            CustomerFullName = "Jane Smith",
                            CustomerTelephone = "+44 20 5555 5555",
                            ShouldBeDoneAt = new DateTime(2023, 4, 28, 10, 34, 40, 534, DateTimeKind.Local).AddTicks(6026),
                            Status = 1
                        });
                });

            modelBuilder.Entity("SAP_API.Models.Oven", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxTempInC")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("Code");

                    b.ToTable("Oven");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000008"),
                            Capacity = 20,
                            Code = "Oven1",
                            MaxTempInC = 250
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Capacity = 25,
                            Code = "Oven2",
                            MaxTempInC = 300
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Capacity = 30,
                            Code = "Oven3",
                            MaxTempInC = 350
                        });
                });

            modelBuilder.Entity("SAP_API.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BakingTempInC")
                        .HasColumnType("integer");

                    b.Property<int>("BakingTimeInMins")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000008"),
                            BakingTempInC = 120,
                            BakingTimeInMins = 30,
                            Name = "Chocolate Croissant",
                            Size = 2
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            BakingTempInC = 140,
                            BakingTimeInMins = 30,
                            Name = "Vanilla Croissant",
                            Size = 1
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            BakingTempInC = 120,
                            BakingTimeInMins = 30,
                            Name = "Pizza",
                            Size = 6
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            BakingTempInC = 200,
                            BakingTimeInMins = 45,
                            Name = "Bagguete",
                            Size = 6
                        });
                });

            modelBuilder.Entity("SAP_API.Models.ProductToPrepare", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BakingProgramId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("QuantityToPrepare")
                        .HasColumnType("integer");

                    b.Property<Guid>("StockLocationId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BakingProgramId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("StockLocationId");

                    b.ToTable("ProductToPrepare");
                });

            modelBuilder.Entity("SAP_API.Models.ReservedOrderProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("PreparedQuantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("ReservedQuantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("StockLocationId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("StockLocationId");

                    b.ToTable("ReservedOrderProduct");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000008"),
                            OrderId = new Guid("00000000-0000-0000-0000-000000000008"),
                            PreparedQuantity = 0,
                            ProductId = new Guid("00000000-0000-0000-0000-000000000008"),
                            ReservedQuantity = 5,
                            StockLocationId = new Guid("00000000-0000-0000-0000-000000000002")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            OrderId = new Guid("00000000-0000-0000-0000-000000000008"),
                            PreparedQuantity = 0,
                            ProductId = new Guid("00000000-0000-0000-0000-000000000008"),
                            ReservedQuantity = 5,
                            StockLocationId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            OrderId = new Guid("00000000-0000-0000-0000-000000000008"),
                            PreparedQuantity = 0,
                            ProductId = new Guid("00000000-0000-0000-0000-000000000008"),
                            ReservedQuantity = 5,
                            StockLocationId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            OrderId = new Guid("00000000-0000-0000-0000-000000000008"),
                            PreparedQuantity = 0,
                            ProductId = new Guid("00000000-0000-0000-0000-000000000002"),
                            ReservedQuantity = 5,
                            StockLocationId = new Guid("00000000-0000-0000-0000-000000000002")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            OrderId = new Guid("00000000-0000-0000-0000-000000000008"),
                            PreparedQuantity = 0,
                            ProductId = new Guid("00000000-0000-0000-0000-000000000002"),
                            ReservedQuantity = 5,
                            StockLocationId = new Guid("00000000-0000-0000-0000-000000000001")
                        });
                });

            modelBuilder.Entity("SAP_API.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Name = "Staff"
                        });
                });

            modelBuilder.Entity("SAP_API.Models.StockLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("Code");

                    b.ToTable("StockLocation");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Capacity = 200,
                            Code = "L1"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Capacity = 100,
                            Code = "L2"
                        });
                });

            modelBuilder.Entity("SAP_API.Models.StockedProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("ReservedQuantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasAlternateKey("ProductId", "LocationId")
                        .HasName("UQ_ProductId_LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("StockedProduct");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000008"),
                            LocationId = new Guid("00000000-0000-0000-0000-000000000002"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000008"),
                            Quantity = 20,
                            ReservedQuantity = 10
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            LocationId = new Guid("00000000-0000-0000-0000-000000000002"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Quantity = 20,
                            ReservedQuantity = 10
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            LocationId = new Guid("00000000-0000-0000-0000-000000000001"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000008"),
                            Quantity = 300,
                            ReservedQuantity = 10
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            LocationId = new Guid("00000000-0000-0000-0000-000000000001"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000002"),
                            Quantity = 20,
                            ReservedQuantity = 10
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            LocationId = new Guid("00000000-0000-0000-0000-000000000002"),
                            ProductId = new Guid("00000000-0000-0000-0000-000000000003"),
                            Quantity = 20,
                            ReservedQuantity = 10
                        });
                });

            modelBuilder.Entity("SAP_API.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Password = "10.Dq24kqmfYfyJ/ZM90uQt3A==.VRQEd9C+pfkWA/sHxLZO9+wEYVMWYMww0MZZIy0nEkQ=",
                            RoleId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Username = "AleksandarAdmin"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Password = "10.Dq24kqmfYfyJ/ZM90uQt3A==.VRQEd9C+pfkWA/sHxLZO9+wEYVMWYMww0MZZIy0nEkQ=",
                            RoleId = new Guid("00000000-0000-0000-0000-000000000002"),
                            Username = "AleksandarStaff"
                        });
                });

            modelBuilder.Entity("SAP_API.Models.BakingProgram", b =>
                {
                    b.HasOne("SAP_API.Models.Oven", "Oven")
                        .WithMany("BakingProgramsPreparedInOven")
                        .HasForeignKey("OvenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SAP_API.Models.User", "PreparedByUser")
                        .WithMany("BakingProgramsMade")
                        .HasForeignKey("PreparedByUserId");

                    b.Navigation("Oven");

                    b.Navigation("PreparedByUser");
                });

            modelBuilder.Entity("SAP_API.Models.BakingProgramProduct", b =>
                {
                    b.HasOne("SAP_API.Models.BakingProgram", "BakingProgram")
                        .WithMany("Products")
                        .HasForeignKey("BakingProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SAP_API.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SAP_API.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BakingProgram");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SAP_API.Models.ProductToPrepare", b =>
                {
                    b.HasOne("SAP_API.Models.BakingProgram", "BakingProgram")
                        .WithMany()
                        .HasForeignKey("BakingProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SAP_API.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SAP_API.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SAP_API.Models.StockLocation", "LocationToPrepareFrom")
                        .WithMany()
                        .HasForeignKey("StockLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BakingProgram");

                    b.Navigation("LocationToPrepareFrom");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SAP_API.Models.ReservedOrderProduct", b =>
                {
                    b.HasOne("SAP_API.Models.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SAP_API.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SAP_API.Models.StockLocation", "LocationWhereProductIsReserved")
                        .WithMany()
                        .HasForeignKey("StockLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocationWhereProductIsReserved");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SAP_API.Models.StockedProduct", b =>
                {
                    b.HasOne("SAP_API.Models.StockLocation", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SAP_API.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SAP_API.Models.User", b =>
                {
                    b.HasOne("SAP_API.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SAP_API.Models.BakingProgram", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SAP_API.Models.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SAP_API.Models.Oven", b =>
                {
                    b.Navigation("BakingProgramsPreparedInOven");
                });

            modelBuilder.Entity("SAP_API.Models.User", b =>
                {
                    b.Navigation("BakingProgramsMade");
                });
#pragma warning restore 612, 618
        }
    }
}
