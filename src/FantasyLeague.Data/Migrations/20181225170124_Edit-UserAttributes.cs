using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class EditUserAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_FavouriteTeamId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "FavouriteTeamId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_FavouriteTeamId",
                table: "AspNetUsers",
                column: "FavouriteTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_FavouriteTeamId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "FavouriteTeamId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_FavouriteTeamId",
                table: "AspNetUsers",
                column: "FavouriteTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
