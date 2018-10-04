using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class changeSalaryToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "JobRole");

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "Employee",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Employee");

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "JobRole",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
