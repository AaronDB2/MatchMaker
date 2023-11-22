using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchMakerBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChallengeTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChallengeTag",
                columns: table => new
                {
                    ChallengesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeTag", x => new { x.ChallengesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ChallengeTag_Challenges_ChallengesId",
                        column: x => x.ChallengesId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChallengeTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70d9b838-d890-476d-a482-99ed5c6345d5", "AQAAAAIAAYagAAAAEJHaWEoIwdINhHHS2UpH/3q1mDpXSuIl3nBlzz68bjvxCBFvsSaqn4aZFtnROdevRg==", "9f920101-dda5-49ea-be1f-23005426a585" });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeTag_TagsId",
                table: "ChallengeTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeTag");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4b374141-5b2c-4db5-8416-01470b1f991e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa16ac3e-df7f-47ac-ab1d-c837cf7f723e", "AQAAAAIAAYagAAAAEEJ15IsPKElv04ad66vnxOaceJ51768cHXrEi2rkAgnrSw/2RFcbg8PBlPSeyk7+Xw==", "de52146a-526c-49c9-b948-ef620fc07697" });
        }
    }
}
