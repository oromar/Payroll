using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class IndexSearchFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Occupation",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Currency",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Occupation_Id_SearchFields",
                table: "Occupation",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Id_SearchFields",
                table: "Currency",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Occupation_Id_SearchFields",
                table: "Occupation");

            migrationBuilder.DropIndex(
                name: "IX_Currency_Id_SearchFields",
                table: "Currency");

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Occupation",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "Currency",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
