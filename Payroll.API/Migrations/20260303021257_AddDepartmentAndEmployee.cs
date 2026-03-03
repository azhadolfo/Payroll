using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentAndEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    employee_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    basic_salary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    department_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee", x => x.id);
                    table.ForeignKey(
                        name: "fk_employee_department_department_id",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_department_name",
                table: "department",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_employee_department_id",
                table: "employee",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "ix_employee_employee_number",
                table: "employee",
                column: "employee_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "department");
        }
    }
}
