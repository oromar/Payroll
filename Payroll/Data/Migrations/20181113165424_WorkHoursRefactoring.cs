using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class WorkHoursRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "WorkHours");

            migrationBuilder.DropColumn(
                name: "End",
                table: "WorkHours");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "WorkHours");

            migrationBuilder.CreateTable(
                name: "WorkHourItem",
                columns: table => new
                {
                    WorkHourItemId = table.Column<Guid>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false),
                    Start = table.Column<TimeSpan>(nullable: false),
                    End = table.Column<TimeSpan>(nullable: false),
                    WorkHoursId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHourItem", x => x.WorkHourItemId);
                    table.ForeignKey(
                        name: "FK_WorkHourItem_WorkHours_WorkHoursId",
                        column: x => x.WorkHoursId,
                        principalTable: "WorkHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkHourItem_WorkHoursId",
                table: "WorkHourItem",
                column: "WorkHoursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkHourItem");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "WorkHours",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "End",
                table: "WorkHours",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Start",
                table: "WorkHours",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
