using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class AlterUser_AddColumnActive_SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 26, 12, 44, 54, 952, DateTimeKind.Local).AddTicks(3612));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "BakingProgrammedAt", "PreparedByUserId" },
                values: new object[] { new DateTime(2023, 4, 26, 12, 44, 54, 952, DateTimeKind.Local).AddTicks(3988), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 26, 12, 44, 54, 951, DateTimeKind.Local).AddTicks(9883));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 27, 12, 44, 54, 943, DateTimeKind.Local).AddTicks(8188));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 28, 12, 44, 54, 943, DateTimeKind.Local).AddTicks(8563));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 26, 12, 44, 54, 934, DateTimeKind.Local).AddTicks(6799));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "Active",
                value: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "Active",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "User");

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 26, 10, 34, 40, 538, DateTimeKind.Local).AddTicks(9406));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "BakingProgrammedAt", "PreparedByUserId" },
                values: new object[] { new DateTime(2023, 4, 26, 10, 34, 40, 538, DateTimeKind.Local).AddTicks(9592), new Guid("00000000-0000-0000-0000-000000000008") });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 26, 10, 34, 40, 538, DateTimeKind.Local).AddTicks(7787));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 27, 10, 34, 40, 534, DateTimeKind.Local).AddTicks(5862));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 28, 10, 34, 40, 534, DateTimeKind.Local).AddTicks(6026));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 26, 10, 34, 40, 526, DateTimeKind.Local).AddTicks(1432));
        }
    }
}
