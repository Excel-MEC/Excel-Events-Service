using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RemoveIsRegisteredFieldFromBookmarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRegistered",
                table: "Bookmarks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRegistered",
                table: "Bookmarks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
