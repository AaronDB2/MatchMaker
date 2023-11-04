using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PasswordReset2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45ccc878-7812-4264-a330-4ec586f83e9a", "AQAAAAIAAYagAAAAEJZEoMS0Jlot1TvUlkBvn8inMe3ARL9iBW3ToWh1bVkZBub34g8Ljwbxx6CJRz70wQ==", "f429e531-2c41-499d-b47d-89d6798195d5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a6a26cb-d0bc-4d76-aa02-a7af365d560c", "AQAAAAIAAYagAAAAEAefAk3F16utr+5aFkBK0A7SW51pAkXDb8cRR7WurZ5zUa7CrMnQx9JgC9RBZuCWtQ==", "0ad43221-8e80-4559-8f32-cad41e3f677a" });
        }
    }
}
