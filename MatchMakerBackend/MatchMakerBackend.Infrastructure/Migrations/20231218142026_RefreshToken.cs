using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "SecurityStamp" },
                values: new object[] { "c3ec39b1-3dfd-4b1b-97bb-8a3e30c404cc", "AQAAAAIAAYagAAAAEMtq+L7DnDQDV99s9mQ2Bz3xLyZgEoVLA+inP6HbKRKOCVui6DRkw9s46FMGRY704g==", null, "ced7c946-e228-400c-9324-55b87a33a8ca" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70d9b838-d890-476d-a482-99ed5c6345d5", "AQAAAAIAAYagAAAAEJHaWEoIwdINhHHS2UpH/3q1mDpXSuIl3nBlzz68bjvxCBFvsSaqn4aZFtnROdevRg==", "9f920101-dda5-49ea-be1f-23005426a585" });
        }
    }
}
