using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class userIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreationUser",
                table: "Currency",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteUser",
                table: "Currency",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdateUser",
                table: "Currency",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationUser",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "DeleteUser",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "LastUpdateUser",
                table: "Currency");
        }
    }
}
