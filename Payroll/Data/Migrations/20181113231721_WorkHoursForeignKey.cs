using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class WorkHoursForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkHourItem_WorkHours_WorkHoursId",
                table: "WorkHourItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkHourItem",
                table: "WorkHourItem");

            migrationBuilder.RenameTable(
                name: "WorkHourItem",
                newName: "WorkHourItems");

            migrationBuilder.RenameIndex(
                name: "IX_WorkHourItem_WorkHoursId",
                table: "WorkHourItems",
                newName: "IX_WorkHourItems_WorkHoursId");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkHoursId",
                table: "WorkHourItems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkHourItems",
                table: "WorkHourItems",
                column: "WorkHourItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHourItems_WorkHours_WorkHoursId",
                table: "WorkHourItems",
                column: "WorkHoursId",
                principalTable: "WorkHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkHourItems_WorkHours_WorkHoursId",
                table: "WorkHourItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkHourItems",
                table: "WorkHourItems");

            migrationBuilder.RenameTable(
                name: "WorkHourItems",
                newName: "WorkHourItem");

            migrationBuilder.RenameIndex(
                name: "IX_WorkHourItems_WorkHoursId",
                table: "WorkHourItem",
                newName: "IX_WorkHourItem_WorkHoursId");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkHoursId",
                table: "WorkHourItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkHourItem",
                table: "WorkHourItem",
                column: "WorkHourItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkHourItem_WorkHours_WorkHoursId",
                table: "WorkHourItem",
                column: "WorkHoursId",
                principalTable: "WorkHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
