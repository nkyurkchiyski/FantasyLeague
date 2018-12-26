using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class EditedRosterEntityPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Matchdays_MatchdayId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_MatchdayId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "MatchdayId",
                table: "Transfers");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchdayId",
                table: "Rosters",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rosters_MatchdayId",
                table: "Rosters",
                column: "MatchdayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rosters_Matchdays_MatchdayId",
                table: "Rosters",
                column: "MatchdayId",
                principalTable: "Matchdays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rosters_Matchdays_MatchdayId",
                table: "Rosters");

            migrationBuilder.DropIndex(
                name: "IX_Rosters_MatchdayId",
                table: "Rosters");

            migrationBuilder.DropColumn(
                name: "MatchdayId",
                table: "Rosters");

            migrationBuilder.AddColumn<Guid>(
                name: "MatchdayId",
                table: "Transfers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_MatchdayId",
                table: "Transfers",
                column: "MatchdayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Matchdays_MatchdayId",
                table: "Transfers",
                column: "MatchdayId",
                principalTable: "Matchdays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
