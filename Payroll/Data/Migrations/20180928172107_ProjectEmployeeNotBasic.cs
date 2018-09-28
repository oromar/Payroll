using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Data.Migrations
{
    public partial class ProjectEmployeeNotBasic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectEmployee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectEmployee");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ProjectEmployee");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ProjectEmployee");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectEmployee");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectEmployee");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProjectEmployee");

            migrationBuilder.DropColumn(
                name: "SearchFields",
                table: "ProjectEmployee");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProjectEmployee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProjectEmployee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectEmployee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectEmployee",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ProjectEmployee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "ProjectEmployee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProjectEmployee",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectEmployee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProjectEmployee",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SearchFields",
                table: "ProjectEmployee",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProjectEmployee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ProjectEmployee",
                nullable: true);
        }
    }
}
