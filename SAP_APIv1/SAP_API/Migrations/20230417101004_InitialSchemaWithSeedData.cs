using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class InitialSchemaWithSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Telephone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BakingTimeInMins = table.Column<int>(type: "integer", nullable: false),
                    BakingTempInC = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.UniqueConstraint("AK_Product_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "StockLocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockLocation", x => x.Id);
                    table.UniqueConstraint("AK_StockLocation_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShouldBeDoneAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockedProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ReservedQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockedProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockedProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockedProduct_StockLocation_LocationId",
                        column: x => x.LocationId,
                        principalTable: "StockLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BakingProgram",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    BakingTimeInMins = table.Column<int>(type: "integer", nullable: false),
                    BakingTempInC = table.Column<int>(type: "integer", nullable: false),
                    BakingProgrammedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    BakingStartedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BakingEndsAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PreparedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RemainingOvenCapacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakingProgram", x => x.Id);
                    table.UniqueConstraint("AK_BakingProgram_Code", x => x.Code);
                    table.ForeignKey(
                        name: "FK_BakingProgram_User_PreparedByUserId",
                        column: x => x.PreparedByUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservedOrderProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    StockLocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservedQuantity = table.Column<int>(type: "integer", nullable: false),
                    PreparedQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservedOrderProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservedOrderProduct_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservedOrderProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservedOrderProduct_StockLocation_StockLocationId",
                        column: x => x.StockLocationId,
                        principalTable: "StockLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BakingProgramProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    BakingProgramId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantityТoBake = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakingProgramProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BakingProgramProduct_BakingProgram_BakingProgramId",
                        column: x => x.BakingProgramId,
                        principalTable: "BakingProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BakingProgramProduct_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BakingProgramProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oven",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    MaxTempInC = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    BakingProgramId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oven", x => x.Id);
                    table.UniqueConstraint("AK_Oven_Code", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Oven_BakingProgram_BakingProgramId",
                        column: x => x.BakingProgramId,
                        principalTable: "BakingProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductToPrepare",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    BakingProgramId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    StockLocationId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantityToPrepare = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductToPrepare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductToPrepare_BakingProgram_BakingProgramId",
                        column: x => x.BakingProgramId,
                        principalTable: "BakingProgram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductToPrepare_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductToPrepare_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductToPrepare_StockLocation_StockLocationId",
                        column: x => x.StockLocationId,
                        principalTable: "StockLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Email", "FullName", "Telephone" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), "janesmith@example.com", "Jane Smith", "+44 20 5555 5555" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "BakingTempInC", "BakingTimeInMins", "Name", "Size" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), 120, 30, "Chocolate Croissant", 2 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), 140, 30, "Vanilla Croissant", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 120, 30, "Pizza", 6 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 200, 45, "Bagguete", 6 }
                });

            migrationBuilder.InsertData(
                table: "StockLocation",
                columns: new[] { "Id", "Capacity", "Code" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 200, "L1" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 100, "L2" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), "12ik12k0", "username" });

            migrationBuilder.InsertData(
                table: "BakingProgram",
                columns: new[] { "Id", "BakingEndsAt", "BakingProgrammedAt", "BakingStartedAt", "BakingTempInC", "BakingTimeInMins", "Code", "CreatedAt", "PreparedByUserId", "RemainingOvenCapacity", "Status" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), null, new DateTime(2023, 4, 17, 12, 10, 3, 839, DateTimeKind.Local).AddTicks(7378), null, 120, 30, "Code1", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000008"), 10, 1 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, new DateTime(2023, 4, 17, 12, 10, 3, 839, DateTimeKind.Local).AddTicks(8913), null, 140, 30, "Code2", new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000008"), 10, 1 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 2, 11, 17, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 17, 12, 10, 3, 839, DateTimeKind.Local).AddTicks(8969), new DateTime(2023, 12, 2, 11, 5, 0, 0, DateTimeKind.Unspecified), 190, 12, "Code3", new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000008"), 10, 5 }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "CustomerId", "ShouldBeDoneAt", "Status" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 4, 17, 12, 10, 3, 833, DateTimeKind.Local).AddTicks(6064), 0 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 4, 18, 12, 10, 3, 836, DateTimeKind.Local).AddTicks(7221), 1 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 4, 19, 12, 10, 3, 836, DateTimeKind.Local).AddTicks(7402), 1 }
                });

            migrationBuilder.InsertData(
                table: "StockedProduct",
                columns: new[] { "Id", "LocationId", "ProductId", "Quantity", "ReservedQuantity" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000008"), 300, 10 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), 20, 10 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), 200, 10 },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000008"), 20, 10 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001"), 20, 10 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000003"), 20, 10 }
                });

            migrationBuilder.InsertData(
                table: "BakingProgramProduct",
                columns: new[] { "Id", "BakingProgramId", "OrderId", "ProductId", "QuantityТoBake" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), 5 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), 5 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), 5 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000002"), 5 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000002"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Oven",
                columns: new[] { "Id", "BakingProgramId", "Capacity", "Code", "MaxTempInC" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), 20, "Oven1", 250 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), 25, "Oven2", 300 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), 30, "Oven3", 350 }
                });

            migrationBuilder.InsertData(
                table: "ReservedOrderProduct",
                columns: new[] { "Id", "OrderId", "PreparedQuantity", "ProductId", "ReservedQuantity", "StockLocationId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), 0, new Guid("00000000-0000-0000-0000-000000000008"), 5, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000008"), 0, new Guid("00000000-0000-0000-0000-000000000008"), 5, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000008"), 0, new Guid("00000000-0000-0000-0000-000000000008"), 5, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000008"), 0, new Guid("00000000-0000-0000-0000-000000000002"), 5, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000008"), 0, new Guid("00000000-0000-0000-0000-000000000002"), 5, new Guid("00000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BakingProgram_PreparedByUserId",
                table: "BakingProgram",
                column: "PreparedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BakingProgramProduct_BakingProgramId",
                table: "BakingProgramProduct",
                column: "BakingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_BakingProgramProduct_OrderId",
                table: "BakingProgramProduct",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_BakingProgramProduct_ProductId",
                table: "BakingProgramProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Oven_BakingProgramId",
                table: "Oven",
                column: "BakingProgramId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductToPrepare_BakingProgramId",
                table: "ProductToPrepare",
                column: "BakingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductToPrepare_OrderId",
                table: "ProductToPrepare",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductToPrepare_ProductId",
                table: "ProductToPrepare",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductToPrepare_StockLocationId",
                table: "ProductToPrepare",
                column: "StockLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservedOrderProduct_OrderId",
                table: "ReservedOrderProduct",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservedOrderProduct_ProductId",
                table: "ReservedOrderProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservedOrderProduct_StockLocationId",
                table: "ReservedOrderProduct",
                column: "StockLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockedProduct_LocationId",
                table: "StockedProduct",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockedProduct_ProductId",
                table: "StockedProduct",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BakingProgramProduct");

            migrationBuilder.DropTable(
                name: "Oven");

            migrationBuilder.DropTable(
                name: "ProductToPrepare");

            migrationBuilder.DropTable(
                name: "ReservedOrderProduct");

            migrationBuilder.DropTable(
                name: "StockedProduct");

            migrationBuilder.DropTable(
                name: "BakingProgram");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "StockLocation");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
