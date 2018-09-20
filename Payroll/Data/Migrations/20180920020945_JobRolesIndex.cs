using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class JobRolesIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "JobRole",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRole_Id_SearchFields",
                table: "JobRole",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobRole_Id_SearchFields",
                table: "JobRole");

            migrationBuilder.AlterColumn<string>(
                name: "SearchFields",
                table: "JobRole",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
