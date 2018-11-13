using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class WorkHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkHours",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    SearchFields = table.Column<string>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false),
                    Start = table.Column<TimeSpan>(nullable: false),
                    End = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkHours_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkHoursEmployee",
                columns: table => new
                {
                    WorkHoursId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHoursEmployee", x => new { x.WorkHoursId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_WorkHoursEmployee_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkHoursEmployee_WorkHours_WorkHoursId",
                        column: x => x.WorkHoursId,
                        principalTable: "WorkHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkHours_CompanyId",
                table: "WorkHours",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHours_Id_SearchFields",
                table: "WorkHours",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHoursEmployee_EmployeeId",
                table: "WorkHoursEmployee",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkHoursEmployee");

            migrationBuilder.DropTable(
                name: "WorkHours");
        }
    }
}
