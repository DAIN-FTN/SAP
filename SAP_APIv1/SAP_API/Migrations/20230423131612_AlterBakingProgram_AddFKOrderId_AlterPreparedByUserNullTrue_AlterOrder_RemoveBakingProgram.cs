using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class AlterBakingProgram_AddFKOrderId_AlterPreparedByUserNullTrue_AlterOrder_RemoveBakingProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BakingProgram_User_PreparedByUserId",
                table: "BakingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_Oven_BakingProgram_BakingProgramId",
                table: "Oven");

            migrationBuilder.DropIndex(
                name: "IX_Oven_BakingProgramId",
                table: "Oven");

            migrationBuilder.DropColumn(
                name: "BakingProgramId",
                table: "Oven");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreparedByUserId",
                table: "BakingProgram",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "OvenId",
                table: "BakingProgram",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "BakingProgrammedAt", "OvenId", "PreparedByUserId" },
                values: new object[] { new DateTime(2023, 4, 23, 15, 16, 11, 13, DateTimeKind.Local).AddTicks(5555), new Guid("00000000-0000-0000-0000-000000000001"), null });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "BakingProgrammedAt", "OvenId" },
                values: new object[] { new DateTime(2023, 4, 23, 15, 16, 11, 13, DateTimeKind.Local).AddTicks(5713), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "BakingProgrammedAt", "OvenId", "PreparedByUserId" },
                values: new object[] { new DateTime(2023, 4, 23, 15, 16, 11, 13, DateTimeKind.Local).AddTicks(3653), new Guid("00000000-0000-0000-0000-000000000008"), null });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 24, 15, 16, 11, 10, DateTimeKind.Local).AddTicks(2573));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 25, 15, 16, 11, 10, DateTimeKind.Local).AddTicks(2721));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 4, 23, 15, 16, 11, 5, DateTimeKind.Local).AddTicks(1297));

            migrationBuilder.CreateIndex(
                name: "IX_BakingProgram_OvenId",
                table: "BakingProgram",
                column: "OvenId");

            migrationBuilder.AddForeignKey(
                name: "FK_BakingProgram_Oven_OvenId",
                table: "BakingProgram",
                column: "OvenId",
                principalTable: "Oven",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BakingProgram_User_PreparedByUserId",
                table: "BakingProgram",
                column: "PreparedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BakingProgram_Oven_OvenId",
                table: "BakingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_BakingProgram_User_PreparedByUserId",
                table: "BakingProgram");

            migrationBuilder.DropIndex(
                name: "IX_BakingProgram_OvenId",
                table: "BakingProgram");

            migrationBuilder.DropColumn(
                name: "OvenId",
                table: "BakingProgram");

            migrationBuilder.AddColumn<Guid>(
                name: "BakingProgramId",
                table: "Oven",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PreparedByUserId",
                table: "BakingProgram",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "BakingProgrammedAt", "PreparedByUserId" },
                values: new object[] { new DateTime(2023, 4, 17, 23, 22, 46, 92, DateTimeKind.Local).AddTicks(777), new Guid("00000000-0000-0000-0000-000000000008") });

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
                columns: new[] { "BakingProgrammedAt", "PreparedByUserId" },
                values: new object[] { new DateTime(2023, 4, 17, 23, 22, 46, 91, DateTimeKind.Local).AddTicks(7986), new Guid("00000000-0000-0000-0000-000000000008") });

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

            migrationBuilder.UpdateData(
                table: "Oven",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgramId",
                value: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "Oven",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "BakingProgramId",
                value: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.UpdateData(
                table: "Oven",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "BakingProgramId",
                value: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.CreateIndex(
                name: "IX_Oven_BakingProgramId",
                table: "Oven",
                column: "BakingProgramId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BakingProgram_User_PreparedByUserId",
                table: "BakingProgram",
                column: "PreparedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Oven_BakingProgram_BakingProgramId",
                table: "Oven",
                column: "BakingProgramId",
                principalTable: "BakingProgram",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
