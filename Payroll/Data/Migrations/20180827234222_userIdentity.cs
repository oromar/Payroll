using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class userIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Currency",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteUser",
                table: "Currency",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Currency",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "DeleteUser",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Currency");
        }
    }
}
