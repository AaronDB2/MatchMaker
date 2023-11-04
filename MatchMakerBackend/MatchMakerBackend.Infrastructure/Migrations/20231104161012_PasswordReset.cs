using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PasswordReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a6a26cb-d0bc-4d76-aa02-a7af365d560c", "AQAAAAIAAYagAAAAEAefAk3F16utr+5aFkBK0A7SW51pAkXDb8cRR7WurZ5zUa7CrMnQx9JgC9RBZuCWtQ==", "0ad43221-8e80-4559-8f32-cad41e3f677a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80bed1da-bd72-4bd2-8442-5ac8f5303073", "AQAAAAIAAYagAAAAEIszbJTn96vWucjUhb7ezkZ0MAkhdPeickEX+hEnBykzoyLt7Rxvb2TORrlmOnX/YA==", "4a619f01-f194-4b0e-bdcb-3f149924bceb" });
        }
    }
}
