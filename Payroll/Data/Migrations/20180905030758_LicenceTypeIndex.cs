using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class LicenceTypeIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "TipoLicenca",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoLicenca_Id_SearchFields",
                table: "TipoLicenca",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TipoLicenca_Id_SearchFields",
                table: "TipoLicenca");

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "TipoLicenca",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
