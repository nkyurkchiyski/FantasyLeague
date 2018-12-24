using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class EditedInviteEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LeagueId",
                table: "Invite",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Invite_LeagueId",
                table: "Invite",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invite_Leagues_LeagueId",
                table: "Invite",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invite_Leagues_LeagueId",
                table: "Invite");

            migrationBuilder.DropIndex(
                name: "IX_Invite_LeagueId",
                table: "Invite");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Invite");
        }
    }
}
