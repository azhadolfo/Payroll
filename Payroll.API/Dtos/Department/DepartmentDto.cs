using Payroll.API.Dtos.Employee;

namespace Payroll.API.Dtos.Department
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
    }
}
