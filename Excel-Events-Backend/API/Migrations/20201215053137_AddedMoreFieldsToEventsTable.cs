using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Migrations
{
    public partial class AddedMoreFieldsToEventsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Button",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationEndDate",
                table: "Events",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationLink",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RegistrationOpen",
                table: "Events",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Button",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RegistrationEndDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RegistrationLink",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RegistrationOpen",
                table: "Events");
        }
    }
}
