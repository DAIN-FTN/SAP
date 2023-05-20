using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAP_API.Migrations
{
    public partial class RemovedBakingProgramsFromSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "BakingProgramProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "BakingProgramProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "BakingProgramProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "BakingProgramProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "BakingProgramProduct",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "BakingProgram",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BakingProgram",
                columns: new[] { "Id", "BakingEndsAt", "BakingProgrammedAt", "BakingStartedAt", "BakingTempInC", "BakingTimeInMins", "Code", "CreatedAt", "OvenId", "PreparedByUserId", "RemainingOvenCapacity", "Status" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), null, new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 120, 30, "Code1", new DateTime(2020, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000001"), 10, 1 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, 140, 30, "Code2", new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), 10, 1 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 2, 11, 17, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 190, 12, "Code3", new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), 10, 5 }
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
        }
    }
}
