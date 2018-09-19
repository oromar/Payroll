using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class CompanyWorkplaceIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Workplace",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Company",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workplace_Id_SearchFields",
                table: "Workplace",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Id_SearchFields",
                table: "Company",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workplace_Id_SearchFields",
                table: "Workplace");

            migrationBuilder.DropIndex(
                name: "IX_Company_Id_SearchFields",
                table: "Company");

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Workplace",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Company",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
