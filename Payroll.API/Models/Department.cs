using System.ComponentModel.DataAnnotations;

namespace Payroll.API.Models
{
    public class Department
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; private set; } = string.Empty;

        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();

        private Department() { } // For EF Core

        public Department(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Department name required.");

            Id = Guid.NewGuid();
            Name = name.Trim().ToUpper();
        }
    }
}
