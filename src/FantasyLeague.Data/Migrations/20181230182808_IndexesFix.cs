using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class IndexesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Players_PlayerId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PlayerId",
                table: "Images",
                column: "PlayerId",
                unique: false,
                filter: "[PlayerId] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_TeamId",
                table: "Images",
                column: "TeamId",
                unique: false,
                filter: "[TeamId] IS NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Players_PlayerId",
                table: "Images",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Players_PlayerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PlayerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TeamId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Players_PlayerId",
                table: "Images",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
