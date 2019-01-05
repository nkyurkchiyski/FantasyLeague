using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class ImagePropertiesRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "TeamImageId",
                table: "Teams",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "PlayerImageId",
                table: "Players",
                newName: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ImageId",
                table: "Teams",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Players_ImageId",
                table: "Players",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Images_ImageId",
                table: "Players",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Images_ImageId",
                table: "Teams",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Images_ImageId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Images_ImageId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ImageId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_ImageId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Teams",
                newName: "TeamImageId");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Players",
                newName: "PlayerImageId");

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
    }
}
