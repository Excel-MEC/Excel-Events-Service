using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventHeads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventHeads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    EventTypeId = table.Column<int>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    Venue = table.Column<string>(nullable: true),
                    Datetime = table.Column<DateTime>(nullable: false),
                    EntryFee = table.Column<int>(nullable: true),
                    PrizeMoney = table.Column<int>(nullable: true),
                    EventHead1Id = table.Column<int>(nullable: false),
                    EventHead2Id = table.Column<int>(nullable: false),
                    IsTeam = table.Column<bool>(nullable: false),
                    TeamSize = table.Column<int>(nullable: true),
                    EventStatusId = table.Column<int>(nullable: false),
                    NumberOfRounds = table.Column<int>(nullable: true),
                    CurrentRound = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_EventHeads_EventHead1Id",
                        column: x => x.EventHead1Id,
                        principalTable: "EventHeads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Events_EventHeads_EventHead2Id",
                        column: x => x.EventHead2Id,
                        principalTable: "EventHeads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventHead1Id",
                table: "Events",
                column: "EventHead1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventHead2Id",
                table: "Events",
                column: "EventHead2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "EventHeads");
        }
    }
}
