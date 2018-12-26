using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class EditedRosterEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Budget",
                table: "Rosters",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Rosters");
        }
    }
}
