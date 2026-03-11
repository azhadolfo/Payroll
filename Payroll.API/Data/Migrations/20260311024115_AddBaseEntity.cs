using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "employees",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "employees",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deleted_by",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "employees",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "employees",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "departments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "departments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "departments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deleted_by",
                table: "departments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "departments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "departments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "departments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "deleted_by",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "deleted_by",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "departments");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "departments");
        }
    }
}
