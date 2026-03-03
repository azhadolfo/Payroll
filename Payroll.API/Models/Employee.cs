using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.API.Models
{
    public class Employee
    {
        public Guid Id { get; private set; }
        [Required]
        [MaxLength(50)]
        public string EmployeeNumber { get; private set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string FirstName { get; private set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string LastName { get; private set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; private set; }

        public bool IsActive { get; private set; } = true;

        //FK
        public Guid DepartmentId { get; private set; }

        public Department Department { get; private set; } = default!;

        private Employee() { } // For EF Core

        public Employee(
            string employeeNumber,
            string firstName,
            string lastName,
            decimal basicSalary,
            Guid departmentId)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name required");

            if (basicSalary <= 0)
                throw new ArgumentException("Salary must be positive");

            Id = Guid.NewGuid();
            EmployeeNumber = employeeNumber;
            FirstName = firstName.Trim().ToUpper();
            LastName = lastName.Trim().ToUpper();
            BasicSalary = basicSalary;
            DepartmentId = departmentId;
        }

        public string FullName => $"{FirstName} {LastName}";

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Employee already inactive.");

            IsActive = false;
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Employee already active.");

            IsActive = true;
        }
    }
}
