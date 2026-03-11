using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.API.Models
{
    public class Employee : BaseEntity
    {
        public Guid Id { get; private set; }

        public string EmployeeNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }

        public bool IsActive { get; set; } = true;

        //FK
        public Guid DepartmentId { get; private set; }

        public Department Department { get; private set; } = default!;

        private Employee()
        { } // For EF Core

        public Employee(string employeeNumber, string firstName, string lastName, decimal salary, Guid departmentId)
        {
            Id = Guid.NewGuid();
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
            SetSalary(salary);
            ChangeDepartment(departmentId);
        }

        public void SetSalary(decimal salary)
        {
            if (salary < 0)
                throw new ArgumentException("Salary must be positive");

            BasicSalary = salary;
        }

        public void ChangeDepartment(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
                throw new ArgumentException("Invalid department");

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