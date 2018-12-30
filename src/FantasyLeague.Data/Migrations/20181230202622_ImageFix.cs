using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class ImageFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Players_PlayerId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Teams_TeamId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PlayerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TeamId",
                table: "Images");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Images_PlayerImageId",
                table: "Players",
                column: "PlayerImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Images_TeamImageId",
                table: "Teams",
                column: "TeamImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Images_PlayerImageId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Images_TeamImageId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_TeamImageId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_PlayerImageId",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PlayerId",
                table: "Images",
                column: "PlayerId",
                unique: true,
                filter: "[PlayerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_TeamId",
                table: "Images",
                column: "TeamId",
                unique: true,
                filter: "[TeamId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Players_PlayerId",
                table: "Images",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Teams_TeamId",
                table: "Images",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
