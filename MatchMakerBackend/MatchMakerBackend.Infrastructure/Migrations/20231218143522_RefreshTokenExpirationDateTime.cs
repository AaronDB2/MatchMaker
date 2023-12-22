using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenExpirationDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpirationDateTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshTokenExpirationDateTime", "SecurityStamp" },
                values: new object[] { "d799e2c6-beb7-4275-aa75-31ff1aa9e1be", "AQAAAAIAAYagAAAAELXIJVpuEWwQffQbHMkOEYEU8VW/nioytEFItZWOyVZS4W7ObNjsDFaRW5KTKdz1aA==", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1e546db0-b542-4344-af5d-46c07b0b45dd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTokenExpirationDateTime",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3ec39b1-3dfd-4b1b-97bb-8a3e30c404cc", "AQAAAAIAAYagAAAAEMtq+L7DnDQDV99s9mQ2Bz3xLyZgEoVLA+inP6HbKRKOCVui6DRkw9s46FMGRY704g==", "ced7c946-e228-400c-9324-55b87a33a8ca" });
        }
    }
}
