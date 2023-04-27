using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class Add_StockedProduct_ProductIdLocationIdAlternateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockedProduct_ProductId",
                table: "StockedProduct");

            migrationBuilder.DeleteData(
                table: "StockedProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.AddUniqueConstraint(
                name: "UQ_ProductId_LocationId",
                table: "StockedProduct",
                columns: new[] { "ProductId", "LocationId" });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 23, 10, 44, 14, 465, DateTimeKind.Local).AddTicks(7917));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 23, 10, 44, 14, 465, DateTimeKind.Local).AddTicks(8027));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 23, 10, 44, 14, 465, DateTimeKind.Local).AddTicks(4437));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 24, 10, 44, 14, 461, DateTimeKind.Local).AddTicks(3740));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 25, 10, 44, 14, 461, DateTimeKind.Local).AddTicks(3968));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 23, 10, 44, 14, 455, DateTimeKind.Local).AddTicks(1949));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "UQ_ProductId_LocationId",
                table: "StockedProduct");

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 22, 13, 46, 4, 557, DateTimeKind.Local).AddTicks(361));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 22, 13, 46, 4, 557, DateTimeKind.Local).AddTicks(502));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 22, 13, 46, 4, 556, DateTimeKind.Local).AddTicks(4864));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 23, 13, 46, 4, 549, DateTimeKind.Local).AddTicks(2508));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 24, 13, 46, 4, 549, DateTimeKind.Local).AddTicks(2841));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 22, 13, 46, 4, 539, DateTimeKind.Local).AddTicks(9357));

            migrationBuilder.InsertData(
                table: "StockedProduct",
                columns: new[] { "Id", "LocationId", "ProductId", "Quantity", "ReservedQuantity" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), 200, 10 });

            migrationBuilder.CreateIndex(
                name: "IX_StockedProduct_ProductId",
                table: "StockedProduct",
                column: "ProductId");
        }
    }
}
