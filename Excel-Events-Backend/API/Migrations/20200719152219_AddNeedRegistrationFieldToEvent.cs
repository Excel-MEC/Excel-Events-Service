using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddNeedRegistrationFieldToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NeedRegistration",
                table: "Events",
                nullable: true,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedRegistration",
                table: "Events");
        }
    }
}
