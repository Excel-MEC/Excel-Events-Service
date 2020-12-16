using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddUniqueConstratintsInTeamAndRegistrationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Registrations_EventId",
                table: "Registrations");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name_EventId",
                table: "Teams",
                columns: new[] { "Name", "EventId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventId_ExcelId",
                table: "Registrations",
                columns: new[] { "EventId", "ExcelId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_Name_EventId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Registrations_EventId_ExcelId",
                table: "Registrations");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventId",
                table: "Registrations",
                column: "EventId");
        }
    }
}
