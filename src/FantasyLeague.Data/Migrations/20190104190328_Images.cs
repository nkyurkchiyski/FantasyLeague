using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamImageId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_PlayerImageId",
                table: "Players");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamImageId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerImageId",
                table: "Players",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamImageId",
                table: "Teams",
                column: "TeamImageId",
                unique: true,
                filter: "[TeamImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerImageId",
                table: "Players",
                column: "PlayerImageId",
                unique: true,
                filter: "[PlayerImageId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamImageId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_PlayerImageId",
                table: "Players");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamImageId",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerImageId",
                table: "Players",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamImageId",
                table: "Teams",
                column: "TeamImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlayerImageId",
                table: "Players",
                column: "PlayerImageId",
                unique: true);
        }
    }
}
