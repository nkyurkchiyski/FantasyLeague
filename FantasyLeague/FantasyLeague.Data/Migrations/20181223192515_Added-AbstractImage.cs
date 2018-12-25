using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class AddedAbstractImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_PlayerId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Players",
                newName: "PlayerImageId");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamImageId",
                table: "Teams",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "ImageType",
                table: "Images",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PlayerId",
                table: "Images",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_TeamId",
                table: "Images",
                column: "TeamId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Teams_TeamId",
                table: "Images",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Teams_TeamId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PlayerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TeamId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TeamImageId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "PlayerImageId",
                table: "Players",
                newName: "ImageId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlayerId",
                table: "Images",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PlayerId",
                table: "Images",
                column: "PlayerId",
                unique: true);
        }
    }
}
