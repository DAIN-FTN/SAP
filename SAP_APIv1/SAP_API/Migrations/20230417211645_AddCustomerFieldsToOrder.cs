using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class AddCustomerFieldsToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Customer_Telephone",
                table: "Order",
                newName: "CustomerTelephone");

            migrationBuilder.RenameColumn(
                name: "Customer_FullName",
                table: "Order",
                newName: "CustomerFullName");

            migrationBuilder.RenameColumn(
                name: "Customer_Email",
                table: "Order",
                newName: "CustomerEmail");

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 23, 16, 42, 572, DateTimeKind.Local).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 23, 16, 42, 572, DateTimeKind.Local).AddTicks(8984));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 23, 16, 42, 572, DateTimeKind.Local).AddTicks(5689));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 18, 23, 16, 42, 568, DateTimeKind.Local).AddTicks(766));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 19, 23, 16, 42, 568, DateTimeKind.Local).AddTicks(1055));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 17, 23, 16, 42, 559, DateTimeKind.Local).AddTicks(797));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerTelephone",
                table: "Order",
                newName: "Customer_Telephone");

            migrationBuilder.RenameColumn(
                name: "CustomerFullName",
                table: "Order",
                newName: "Customer_FullName");

            migrationBuilder.RenameColumn(
                name: "CustomerEmail",
                table: "Order",
                newName: "Customer_Email");

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
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 18, 22, 50, 3, 894, DateTimeKind.Local).AddTicks(3023));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 19, 22, 50, 3, 894, DateTimeKind.Local).AddTicks(3232));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 17, 22, 50, 3, 887, DateTimeKind.Local).AddTicks(8142));
        }
    }
}
