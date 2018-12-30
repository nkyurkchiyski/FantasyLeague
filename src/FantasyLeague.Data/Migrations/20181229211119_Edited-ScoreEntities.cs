using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class EditedScoreEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RedCards",
                table: "Scores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YellowCards",
                table: "Scores",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedCards",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "YellowCards",
                table: "Scores");
        }
    }
}
