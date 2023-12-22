using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompanyManagerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8bdf1de4-abfc-4a1b-84c0-6dff73bbf00b"), "8bdf1de4-abfc-4a1b-84c0-6dff73bbf00b", "CompanyManager", "COMPANYMANAGER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9e0b161-36f9-497b-82af-1822241b9431", "AQAAAAIAAYagAAAAEIik9IUPCtgpAzyk7TYmHieAC5r1XkTCCaq7CGHuNY88AUsF5GffGss7lhAnjQPqeg==", "c93ccc04-28f1-4ee4-874a-e2de56b590bb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8bdf1de4-abfc-4a1b-84c0-6dff73bbf00b"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d799e2c6-beb7-4275-aa75-31ff1aa9e1be", "AQAAAAIAAYagAAAAELXIJVpuEWwQffQbHMkOEYEU8VW/nioytEFItZWOyVZS4W7ObNjsDFaRW5KTKdz1aA==", "1e546db0-b542-4344-af5d-46c07b0b45dd" });
        }
    }
}
