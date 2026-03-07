using Payroll.API.Dtos.Employee;
using Payroll.API.Models;

namespace Payroll.API.Mappers
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