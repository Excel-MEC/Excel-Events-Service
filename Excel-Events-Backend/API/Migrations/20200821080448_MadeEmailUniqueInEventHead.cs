using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class MadeEmailUniqueInEventHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EventHeads_Email",
                table: "EventHeads",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventHeads_Email",
                table: "EventHeads");
        }
    }
}
