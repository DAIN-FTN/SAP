using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class RemoveCustomerDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "Customer_Email",
                table: "Order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_FullName",
                table: "Order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Telephone",
                table: "Order",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 22, 50, 3, 899, DateTimeKind.Local).AddTicks(6519));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 22, 50, 3, 899, DateTimeKind.Local).AddTicks(6681));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 22, 50, 3, 899, DateTimeKind.Local).AddTicks(3626));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CustomerId", "ShouldBeDoneAt", "Customer_Email", "Customer_FullName", "Customer_Telephone" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 4, 18, 22, 50, 3, 894, DateTimeKind.Local).AddTicks(3023), "janesmith@example.com", "Jane Smith", "+44 20 5555 5555" });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CustomerId", "ShouldBeDoneAt", "Customer_Email", "Customer_FullName", "Customer_Telephone" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 4, 19, 22, 50, 3, 894, DateTimeKind.Local).AddTicks(3232), "janesmith@example.com", "Jane Smith", "+44 20 5555 5555" });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "CustomerId", "ShouldBeDoneAt", "Customer_Email", "Customer_FullName", "Customer_Telephone" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 4, 17, 22, 50, 3, 887, DateTimeKind.Local).AddTicks(8142), "janesmith@example.com", "Jane Smith", "+44 20 5555 5555" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer_Email",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Customer_FullName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Customer_Telephone",
                table: "Order");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Telephone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 12, 10, 3, 839, DateTimeKind.Local).AddTicks(8913));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 12, 10, 3, 839, DateTimeKind.Local).AddTicks(8969));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 12, 10, 3, 839, DateTimeKind.Local).AddTicks(7378));

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Email", "FullName", "Telephone" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), "janesmith@example.com", "Jane Smith", "+44 20 5555 5555" });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CustomerId", "ShouldBeDoneAt" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 4, 18, 12, 10, 3, 836, DateTimeKind.Local).AddTicks(7221) });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "CustomerId", "ShouldBeDoneAt" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 4, 19, 12, 10, 3, 836, DateTimeKind.Local).AddTicks(7402) });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "CustomerId", "ShouldBeDoneAt" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 4, 17, 12, 10, 3, 833, DateTimeKind.Local).AddTicks(6064) });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
