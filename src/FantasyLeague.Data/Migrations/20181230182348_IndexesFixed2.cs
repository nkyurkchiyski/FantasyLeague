using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class IndexesFixed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_PlayerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TeamId",
                table: "Images");
        }
    }
}
