using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class AddedStatusProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSub",
                table: "RostersPlayers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MatchdayStatus",
                table: "Matchdays",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "TransferWindowStatus",
                table: "Matchdays",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSub",
                table: "RostersPlayers");

            migrationBuilder.DropColumn(
                name: "MatchdayStatus",
                table: "Matchdays");

            migrationBuilder.DropColumn(
                name: "TransferWindowStatus",
                table: "Matchdays");
        }
    }
}
