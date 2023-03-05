using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("00000000-0000-0000-0000-000000000008"), null, new DateTime(2023, 3, 5, 20, 38, 46, 976, DateTimeKind.Local).AddTicks(974), null, 120, 30, "Code1", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000008"), 10, 1 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, new DateTime(2023, 3, 5, 20, 38, 46, 976, DateTimeKind.Local).AddTicks(3160), null, 140, 30, "Code2", new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000008"), 10, 1 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 2, 11, 17, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 5, 20, 38, 46, 976, DateTimeKind.Local).AddTicks(3240), new DateTime(2023, 12, 2, 11, 5, 0, 0, DateTimeKind.Unspecified), 190, 12, "Code3", new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000008"), 10, 5 }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "CustomerId", "ShouldBeDoneAt", "Status" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 3, 5, 20, 38, 46, 967, DateTimeKind.Local).AddTicks(7567), 0 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 3, 6, 20, 38, 46, 973, DateTimeKind.Local).AddTicks(1742), 1 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 3, 7, 20, 38, 46, 973, DateTimeKind.Local).AddTicks(1919), 1 }
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Oven",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Oven",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Oven",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "ReservedOrderProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ReservedOrderProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "ReservedOrderProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "ReservedOrderProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "ReservedOrderProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "StockedProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "StockedProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "StockedProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "StockedProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "StockedProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "StockedProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "StockLocation",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "StockLocation",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));
        }
    }
}
