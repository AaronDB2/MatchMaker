using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class impactMakerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("da3d90b2-4900-4883-bfb6-89d7e83d748a"), "da3d90b2-4900-4883-bfb6-89d7e83d748a", "ImpactMaker", "IMPACTMAKER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa16ac3e-df7f-47ac-ab1d-c837cf7f723e", "AQAAAAIAAYagAAAAEEJ15IsPKElv04ad66vnxOaceJ51768cHXrEi2rkAgnrSw/2RFcbg8PBlPSeyk7+Xw==", "de52146a-526c-49c9-b948-ef620fc07697" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("da3d90b2-4900-4883-bfb6-89d7e83d748a"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d062ed57-a9bf-488e-81d2-155b405bc7a5", "AQAAAAIAAYagAAAAENgQTcwEwGDrXt1YdapDEJMK5lIrQfokilOBG9mcbRZj09zIA4rltVTkjKkjjlljNg==", "cc6085cd-96de-4ef0-8543-b59f925e515d" });
        }
    }
}
