using System.ComponentModel.DataAnnotations;

namespace Payroll.API.Features.Employees.Dtos
{
    public class EmployeeResponseDto
    {
        public Guid Id { get; set; }
        public string EmployeeNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal BasicSalary { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
