using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixingUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "9c053325-e973-4e65-955e-d15859daa97d", "AARONTEST@GMAIL.COM", "AQAAAAIAAYagAAAAEG8BQiNJ0ziA3HGOmiE2dI+XWWKkaiOlJMWyF2kzqV0D0snVeCMLjF7La8/mn9fveg==", "bd312f51-446a-44bc-8a2e-73d1b13bb5da", "aarontest@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "45ccc878-7812-4264-a330-4ec586f83e9a", "AARONTEST", "AQAAAAIAAYagAAAAEJZEoMS0Jlot1TvUlkBvn8inMe3ARL9iBW3ToWh1bVkZBub34g8Ljwbxx6CJRz70wQ==", "f429e531-2c41-499d-b47d-89d6798195d5", "AaronTest" });
        }
    }
}
