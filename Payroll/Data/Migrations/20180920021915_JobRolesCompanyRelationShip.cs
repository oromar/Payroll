using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class JobRolesCompanyRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JobRole",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JobRole_CompanyId",
                table: "JobRole",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRole_Company_CompanyId",
                table: "JobRole",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRole_Company_CompanyId",
                table: "JobRole");

            migrationBuilder.DropIndex(
                name: "IX_JobRole_CompanyId",
                table: "JobRole");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JobRole");
        }
    }
}
