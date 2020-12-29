using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddMoreFieldsToResultsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExcelId",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcelId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Results");
        }
    }
}
