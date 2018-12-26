using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class Edited2FixtureWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Winner",
                table: "Fixtures",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Winner",
                table: "Fixtures",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
