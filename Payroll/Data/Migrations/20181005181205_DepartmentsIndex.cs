using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class DepartmentsIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Department",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_Id_SearchFields",
                table: "Department",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Department_Id_SearchFields",
                table: "Department");

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Department",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
