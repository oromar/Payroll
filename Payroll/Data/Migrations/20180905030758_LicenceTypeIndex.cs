using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class LicenceTypeIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "LicenseType",
                nullable: true,
                maxLength: 450,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LicenseType_Id_SearchFields",
                table: "LicenseType",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LicenseType_Id_SearchFields",
                table: "LicenseType");

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "LicenseType",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
