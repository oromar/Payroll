using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class projectResponsible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResponsibleId",
                table: "Project",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Project_ResponsibleId",
                table: "Project",
                column: "ResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Employee_ResponsibleId",
                table: "Project",
                column: "ResponsibleId",
                principalTable: "Employee",
                principalColumn: "Id",
                onUpdate: ReferentialAction.NoAction,
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Employee_ResponsibleId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ResponsibleId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Project");
        }
    }
}
