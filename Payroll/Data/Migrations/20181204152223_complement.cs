using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class complement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "Workplace",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "Company",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complement",
                table: "Workplace");

            migrationBuilder.DropColumn(
                name: "Complement",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Complement",
                table: "Company");
        }
    }
}
