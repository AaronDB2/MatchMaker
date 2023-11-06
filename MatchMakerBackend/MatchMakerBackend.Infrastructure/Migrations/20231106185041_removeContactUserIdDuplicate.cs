using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeContactUserIdDuplicate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactUserId",
                table: "Challenges");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d062ed57-a9bf-488e-81d2-155b405bc7a5", "AQAAAAIAAYagAAAAENgQTcwEwGDrXt1YdapDEJMK5lIrQfokilOBG9mcbRZj09zIA4rltVTkjKkjjlljNg==", "cc6085cd-96de-4ef0-8543-b59f925e515d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContactUserId",
                table: "Challenges",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c053325-e973-4e65-955e-d15859daa97d", "AQAAAAIAAYagAAAAEG8BQiNJ0ziA3HGOmiE2dI+XWWKkaiOlJMWyF2kzqV0D0snVeCMLjF7La8/mn9fveg==", "bd312f51-446a-44bc-8a2e-73d1b13bb5da" });
        }
    }
}
