using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class SearchFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchFields",
                table: "Occupation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SearchFields",
                table: "Currency",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchFields",
                table: "Occupation");

            migrationBuilder.DropColumn(
                name: "SearchFields",
                table: "Currency");
        }
    }
}
