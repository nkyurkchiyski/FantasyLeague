using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class EditedFixtureEntityResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "Fixtures");

            migrationBuilder.AddColumn<int>(
                name: "Winner",
                table: "Fixtures",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Fixtures");

            migrationBuilder.AddColumn<int>(
                name: "Result",
                table: "Fixtures",
                nullable: false,
                defaultValue: 0);
        }
    }
}
