using Payroll.API.Features.Employees.Dtos;
using Payroll.API.Models;

namespace Payroll.API.Features.Employees
{
    public static class EmployeeMapper
    {
        public static EmployeeResponseDto ToDto(this Employee employee)
        {
            return new EmployeeResponseDto
            {
                Id = employee.Id,
                EmployeeNumber = employee.EmployeeNumber,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BasicSalary = employee.BasicSalary,
                DepartmentId = employee.DepartmentId
            };
        }

        public static Employee ToEmployeeFromCreateDto(this EmployeeCreateDto createEmployeeDto)
        {
            return new Employee(
                createEmployeeDto.EmployeeNumber,
                createEmployeeDto.FirstName,
                createEmployeeDto.LastName,
                createEmployeeDto.BasicSalary,
                createEmployeeDto.DepartmentId);
        }
    }
}