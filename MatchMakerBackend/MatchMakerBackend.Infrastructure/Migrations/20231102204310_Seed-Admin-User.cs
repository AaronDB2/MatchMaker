using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("d5873591-4ef5-4d3b-a570-7e8b50748ba9"), "d5873591-4ef5-4d3b-a570-7e8b50748ba9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("6fba5be2-7f0a-496e-8090-f02c71b645d8"), 0, null, "3c774981-8de8-4a97-bc0d-3c9d9e0e326e", "aarontest@gmail.com", true, false, null, null, "AARONTEST", "AQAAAAIAAYagAAAAEDRP1SoHptzPdaaVcP4kt1UeEN/+3DHJj1S3XRqEzwOUhokTqNh4tKYrj7VSA+QozA==", null, false, null, false, "AaronTest" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d5873591-4ef5-4d3b-a570-7e8b50748ba9"), new Guid("6fba5be2-7f0a-496e-8090-f02c71b645d8") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d5873591-4ef5-4d3b-a570-7e8b50748ba9"), new Guid("6fba5be2-7f0a-496e-8090-f02c71b645d8") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d5873591-4ef5-4d3b-a570-7e8b50748ba9"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6fba5be2-7f0a-496e-8090-f02c71b645d8"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
