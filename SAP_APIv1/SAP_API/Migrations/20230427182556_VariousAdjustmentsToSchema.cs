using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class VariousAdjustmentsToSchema : Migration
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

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DropColumn(
                name: "BakingProgramId",
                table: "Oven");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "User",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2023, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ShouldBeDoneAt",
                value: new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, "Admin" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), null, "Staff" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "10.Dq24kqmfYfyJ/ZM90uQt3A==.VRQEd9C+pfkWA/sHxLZO9+wEYVMWYMww0MZZIy0nEkQ=", new Guid("00000000-0000-0000-0000-000000000001"), "AleksandarAdmin" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "10.Dq24kqmfYfyJ/ZM90uQt3A==.VRQEd9C+pfkWA/sHxLZO9+wEYVMWYMww0MZZIy0nEkQ=", new Guid("00000000-0000-0000-0000-000000000002"), "AleksandarStaff" }
                });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "BakingProgrammedAt", "OvenId", "PreparedByUserId" },
                values: new object[] { new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001") });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "BakingProgrammedAt", "BakingStartedAt", "OvenId", "PreparedByUserId" },
                values: new object[] { new DateTime(2020, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "BakingProgrammedAt", "OvenId", "PreparedByUserId" },
                values: new object[] { new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000001") });

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BakingProgram_OvenId",
                table: "BakingProgram",
                column: "OvenId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BakingProgram_Oven_OvenId",
                table: "BakingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_BakingProgram_User_PreparedByUserId",
                table: "BakingProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_BakingProgram_OvenId",
                table: "BakingProgram");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "User");

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

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), "12ik12k0", "username" });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "BakingProgrammedAt", "PreparedByUserId" },
                values: new object[] { new DateTime(2023, 4, 23, 10, 44, 14, 465, DateTimeKind.Local).AddTicks(7917), new Guid("00000000-0000-0000-0000-000000000008") });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "BakingProgrammedAt", "BakingStartedAt", "PreparedByUserId" },
                values: new object[] { new DateTime(2023, 4, 23, 10, 44, 14, 465, DateTimeKind.Local).AddTicks(8027), new DateTime(2023, 12, 2, 11, 5, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000008") });

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "BakingProgrammedAt", "PreparedByUserId" },
                values: new object[] { new DateTime(2023, 4, 23, 10, 44, 14, 465, DateTimeKind.Local).AddTicks(4437), new Guid("00000000-0000-0000-0000-000000000008") });

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
