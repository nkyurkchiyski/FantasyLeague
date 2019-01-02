using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyLeague.Data.Migrations
{
    public partial class RemovedInviteLeagues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invite");

            migrationBuilder.DropTable(
                name: "UsersLeagues");

            migrationBuilder.DropTable(
                name: "Leagues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leagues_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invite",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InviteeId = table.Column<string>(nullable: false),
                    InviterId = table.Column<string>(nullable: false),
                    LeagueId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invite_AspNetUsers_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invite_AspNetUsers_InviterId",
                        column: x => x.InviterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invite_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersLeagues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeagueId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLeagues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersLeagues_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersLeagues_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invite_InviteeId",
                table: "Invite",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_InviterId",
                table: "Invite",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_LeagueId",
                table: "Invite",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_CreatorId",
                table: "Leagues",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_Name",
                table: "Leagues",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersLeagues_LeagueId",
                table: "UsersLeagues",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLeagues_UserId",
                table: "UsersLeagues",
                column: "UserId");
        }
    }
}
