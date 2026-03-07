using Payroll.API.Dtos.Employee;

namespace Payroll.API.Dtos.Department
{
    public class DepartmentResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<EmployeeResponseDto> Employees { get; set; } = new List<EmployeeResponseDto>();
    }
}