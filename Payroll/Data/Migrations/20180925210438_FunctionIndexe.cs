using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class FunctionIndexe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Function",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Function_Id_SearchFields",
                table: "Function",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Function_Id_SearchFields",
                table: "Function");

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Function",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
