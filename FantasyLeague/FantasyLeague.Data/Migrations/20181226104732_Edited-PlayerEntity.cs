using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class EditedPlayerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Injured",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Players",
                newName: "Nationality");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Players",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "Players",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Players",
                newName: "FirstName");

            migrationBuilder.AddColumn<bool>(
                name: "Injured",
                table: "Players",
                nullable: false,
                defaultValue: false);
        }
    }
}
