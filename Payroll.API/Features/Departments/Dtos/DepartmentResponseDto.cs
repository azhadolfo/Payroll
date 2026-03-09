using Payroll.API.Features.Employees.Dtos;

namespace Payroll.API.Features.Departments.Dtos
{
    public class DepartmentResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<EmployeeResponseDto> Employees { get; set; } = new List<EmployeeResponseDto>();
    }
}