using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class ChangedBasicEntityColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Occupation",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "DeleteUser",
                table: "Occupation",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteTime",
                table: "Occupation",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Currency",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "DeleteUser",
                table: "Currency",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "DeleteTime",
                table: "Currency",
                newName: "DeletedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Occupation",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Occupation",
                newName: "DeleteUser");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Occupation",
                newName: "DeleteTime");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Currency",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Currency",
                newName: "DeleteUser");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Currency",
                newName: "DeleteTime");
        }
    }
}
