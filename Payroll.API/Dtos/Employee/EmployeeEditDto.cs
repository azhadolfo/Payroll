namespace Payroll.API.Dtos.Employee
{
    public class EmployeeEditDto
    {
        public string EmployeeNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal BasicSalary { get; set; }
        public Guid DepartmentId { get; set; }
    }
}