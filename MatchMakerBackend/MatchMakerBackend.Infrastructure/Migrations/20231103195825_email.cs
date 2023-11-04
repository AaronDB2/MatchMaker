using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6fba5be2-7f0a-496e-8090-f02c71b645d8"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "0042a497-d0c6-4ddc-a6ce-3bf5f245f8ec", "AARONTEST@GMAIL.COM", "AQAAAAIAAYagAAAAEFK6in1VIDrBDPNdOnVECpL9JWkG/SVeWEqF++KLH/s3oWmM+ZI/HvUggQV9gjWMrg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6fba5be2-7f0a-496e-8090-f02c71b645d8"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "3c774981-8de8-4a97-bc0d-3c9d9e0e326e", null, "AQAAAAIAAYagAAAAEDRP1SoHptzPdaaVcP4kt1UeEN/+3DHJj1S3XRqEzwOUhokTqNh4tKYrj7VSA+QozA==" });
        }
    }
}
