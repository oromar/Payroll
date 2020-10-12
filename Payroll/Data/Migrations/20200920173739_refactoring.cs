using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workplace_Id_SearchFields",
                table: "Workplace");

            migrationBuilder.DropIndex(
                name: "IX_WorkHours_Id_SearchFields",
                table: "WorkHours");

            migrationBuilder.DropIndex(
                name: "IX_Vacation_Id_SearchFields",
                table: "Vacation");

            migrationBuilder.DropIndex(
                name: "IX_Project_Id_SearchFields",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_OccurrenceType_Id_SearchFields",
                table: "OccurrenceType");

            migrationBuilder.DropIndex(
                name: "IX_Occupation_Id_SearchFields",
                table: "Occupation");

            migrationBuilder.DropIndex(
                name: "IX_LicenseType_Id_SearchFields",
                table: "LicenseType");

            migrationBuilder.DropIndex(
                name: "IX_JobRole_Id_SearchFields",
                table: "JobRole");

            migrationBuilder.DropIndex(
                name: "IX_Function_Id_SearchFields",
                table: "Function");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Id_SearchFields",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Department_Id_SearchFields",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Currency_Id_SearchFields",
                table: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Company_Id_SearchFields",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "Workplace",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "WorkHours",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "Vacation",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "Project",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "OccurrenceType",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "Occupation",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "LicenseType",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "JobRole",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "Function",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "EmployeeHistory",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "Employee",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "Department",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "Currency",
                newName: "SearchText");

            migrationBuilder.RenameColumn(
                name: "SearchFields",
                table: "Company",
                newName: "SearchText");

            migrationBuilder.CreateIndex(
                name: "IX_Workplace_Id_SearchText",
                table: "Workplace",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHours_Id_SearchText",
                table: "WorkHours",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vacation_Id_SearchText",
                table: "Vacation",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Id_SearchText",
                table: "Project",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OccurrenceType_Id_SearchText",
                table: "OccurrenceType",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Occupation_Id_SearchText",
                table: "Occupation",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseType_Id_SearchText",
                table: "LicenseType",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobRole_Id_SearchText",
                table: "JobRole",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Function_Id_SearchText",
                table: "Function",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Id_SearchText",
                table: "Employee",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Id_SearchText",
                table: "Department",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Id_SearchText",
                table: "Currency",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Id_SearchText",
                table: "Company",
                columns: new[] { "Id", "SearchText" },
                unique: true,
                filter: "[SearchText] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workplace_Id_SearchText",
                table: "Workplace");

            migrationBuilder.DropIndex(
                name: "IX_WorkHours_Id_SearchText",
                table: "WorkHours");

            migrationBuilder.DropIndex(
                name: "IX_Vacation_Id_SearchText",
                table: "Vacation");

            migrationBuilder.DropIndex(
                name: "IX_Project_Id_SearchText",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_OccurrenceType_Id_SearchText",
                table: "OccurrenceType");

            migrationBuilder.DropIndex(
                name: "IX_Occupation_Id_SearchText",
                table: "Occupation");

            migrationBuilder.DropIndex(
                name: "IX_LicenseType_Id_SearchText",
                table: "LicenseType");

            migrationBuilder.DropIndex(
                name: "IX_JobRole_Id_SearchText",
                table: "JobRole");

            migrationBuilder.DropIndex(
                name: "IX_Function_Id_SearchText",
                table: "Function");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Id_SearchText",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Department_Id_SearchText",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Currency_Id_SearchText",
                table: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Company_Id_SearchText",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "Workplace",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "WorkHours",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "Vacation",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "Project",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "OccurrenceType",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "Occupation",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "LicenseType",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "JobRole",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "Function",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "EmployeeHistory",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "Employee",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "Department",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "Currency",
                newName: "SearchFields");

            migrationBuilder.RenameColumn(
                name: "SearchText",
                table: "Company",
                newName: "SearchFields");

            migrationBuilder.CreateIndex(
                name: "IX_Workplace_Id_SearchFields",
                table: "Workplace",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHours_Id_SearchFields",
                table: "WorkHours",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vacation_Id_SearchFields",
                table: "Vacation",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Id_SearchFields",
                table: "Project",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OccurrenceType_Id_SearchFields",
                table: "OccurrenceType",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Occupation_Id_SearchFields",
                table: "Occupation",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LicenseType_Id_SearchFields",
                table: "LicenseType",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobRole_Id_SearchFields",
                table: "JobRole",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Function_Id_SearchFields",
                table: "Function",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Id_SearchFields",
                table: "Employee",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Id_SearchFields",
                table: "Department",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Id_SearchFields",
                table: "Currency",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Id_SearchFields",
                table: "Company",
                columns: new[] { "Id", "SearchFields" },
                unique: true,
                filter: "[SearchFields] IS NOT NULL");
        }
    }
}
