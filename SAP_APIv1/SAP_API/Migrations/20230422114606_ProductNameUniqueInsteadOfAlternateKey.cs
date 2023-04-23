using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class ProductNameUniqueInsteadOfAlternateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Product_Name",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                table: "Product",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_Name",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Product_Name",
                table: "Product",
                column: "Name");

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 23, 22, 46, 92, DateTimeKind.Local).AddTicks(777));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 23, 22, 46, 92, DateTimeKind.Local).AddTicks(883));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 17, 23, 22, 46, 91, DateTimeKind.Local).AddTicks(7986));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 18, 23, 22, 46, 84, DateTimeKind.Local).AddTicks(3975));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 19, 23, 22, 46, 84, DateTimeKind.Local).AddTicks(4182));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 17, 23, 22, 46, 78, DateTimeKind.Local).AddTicks(3646));
        }
    }
}
