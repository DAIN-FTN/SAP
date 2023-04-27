using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class CreateRole_AlterUserAddRoleFK_SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "User",
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
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 26, 10, 34, 40, 538, DateTimeKind.Local).AddTicks(9406));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 26, 10, 34, 40, 538, DateTimeKind.Local).AddTicks(9592));

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

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

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
                name: "FK_User_Role_RoleId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_User_RoleId",
                table: "User");

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

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 23, 15, 16, 11, 13, DateTimeKind.Local).AddTicks(5555));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 23, 15, 16, 11, 13, DateTimeKind.Local).AddTicks(5713));

            migrationBuilder.UpdateData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "BakingProgrammedAt",
                value: new DateTime(2023, 4, 23, 15, 16, 11, 13, DateTimeKind.Local).AddTicks(3653));

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

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000008"), "12ik12k0", "username" });
        }
    }
}
