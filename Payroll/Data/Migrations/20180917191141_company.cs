using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LicenseType",
                table: "LicenseType");

            migrationBuilder.RenameTable(
                name: "LicenseType",
                newName: "LicenseType");

            migrationBuilder.RenameIndex(
                name: "IX_LicenseType_Id_SearchFields",
                table: "LicenseType",
                newName: "IX_LicenseType_Id_SearchFields");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LicenseType",
                table: "LicenseType",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Company",
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
                    PersonalJuridicalName = table.Column<string>(nullable: false),
                    SocialReason = table.Column<string>(nullable: false),
                    OccupationArea = table.Column<string>(nullable: false),
                    FoundationDate = table.Column<DateTime>(nullable: false),
                    Nacionality = table.Column<string>(nullable: false),
                    HasStrangers = table.Column<string>(nullable: false),
                    CurrencyId = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Neighborhood = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_CurrencyId",
                table: "Company",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LicenseType",
                table: "LicenseType");

            migrationBuilder.RenameTable(
                name: "LicenseType",
                newName: "LicenseType");

            migrationBuilder.RenameIndex(
                name: "IX_LicenseType_Id_SearchFields",
                table: "LicenseType",
                newName: "IX_LicenseType_Id_SearchFields");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LicenseType",
                table: "LicenseType",
                column: "Id");
        }
    }
}
