using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.API.Migrations
{
    /// <inheritdoc />
    public partial class MigrationToLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employee_department_department_id",
                table: "employee");

            migrationBuilder.DropPrimaryKey(
                name: "pk_employee",
                table: "employee");

            migrationBuilder.DropPrimaryKey(
                name: "pk_department",
                table: "department");

            migrationBuilder.RenameTable(
                name: "employee",
                newName: "employees");

            migrationBuilder.RenameTable(
                name: "department",
                newName: "departments");

            migrationBuilder.RenameIndex(
                name: "ix_employee_employee_number",
                table: "employees",
                newName: "ix_employees_employee_number");

            migrationBuilder.RenameIndex(
                name: "ix_employee_department_id",
                table: "employees",
                newName: "ix_employees_department_id");

            migrationBuilder.RenameIndex(
                name: "ix_department_name",
                table: "departments",
                newName: "ix_departments_name");

            migrationBuilder.AddPrimaryKey(
                name: "pk_employees",
                table: "employees",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_departments",
                table: "departments",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_departments_department_id",
                table: "employees",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_departments_department_id",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "pk_employees",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "pk_departments",
                table: "departments");

            migrationBuilder.RenameTable(
                name: "employees",
                newName: "employee");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "department");

            migrationBuilder.RenameIndex(
                name: "ix_employees_employee_number",
                table: "employee",
                newName: "ix_employee_employee_number");

            migrationBuilder.RenameIndex(
                name: "ix_employees_department_id",
                table: "employee",
                newName: "ix_employee_department_id");

            migrationBuilder.RenameIndex(
                name: "ix_departments_name",
                table: "department",
                newName: "ix_department_name");

            migrationBuilder.AddPrimaryKey(
                name: "pk_employee",
                table: "employee",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_department",
                table: "department",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employee_department_department_id",
                table: "employee",
                column: "department_id",
                principalTable: "department",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
