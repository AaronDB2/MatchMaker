using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecurityStamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d5873591-4ef5-4d3b-a570-7e8b50748ba9"), new Guid("6fba5be2-7f0a-496e-8090-f02c71b645d8") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6fba5be2-7f0a-496e-8090-f02c71b645d8"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"), 0, null, "80bed1da-bd72-4bd2-8442-5ac8f5303073", "aarontest@gmail.com", true, false, null, "AARONTEST@GMAIL.COM", "AARONTEST", "AQAAAAIAAYagAAAAEIszbJTn96vWucjUhb7ezkZ0MAkhdPeickEX+hEnBykzoyLt7Rxvb2TORrlmOnX/YA==", null, false, "4a619f01-f194-4b0e-bdcb-3f149924bceb", false, "AaronTest" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d5873591-4ef5-4d3b-a570-7e8b50748ba9"), new Guid("4b374141-5b2c-4db5-8416-01470b1f991e") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d5873591-4ef5-4d3b-a570-7e8b50748ba9"), new Guid("4b374141-5b2c-4db5-8416-01470b1f991e") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("6fba5be2-7f0a-496e-8090-f02c71b645d8"), 0, null, "0042a497-d0c6-4ddc-a6ce-3bf5f245f8ec", "aarontest@gmail.com", true, false, null, "AARONTEST@GMAIL.COM", "AARONTEST", "AQAAAAIAAYagAAAAEFK6in1VIDrBDPNdOnVECpL9JWkG/SVeWEqF++KLH/s3oWmM+ZI/HvUggQV9gjWMrg==", null, false, null, false, "AaronTest" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d5873591-4ef5-4d3b-a570-7e8b50748ba9"), new Guid("6fba5be2-7f0a-496e-8090-f02c71b645d8") });
        }
    }
}
