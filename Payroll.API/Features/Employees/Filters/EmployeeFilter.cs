using Payroll.API.Common;

namespace Payroll.API.Features.Employees.Filters
{
    public class EmployeeFilter : PaginationQuery
    {
        private string? firstName;

        public string? FirstName
        {
            get => firstName;
            set => firstName = value?.Trim().ToLower();
        }

        private string? lastName;

        public string? LastName
        {
            get => lastName;
            set => lastName = value?.Trim().ToLower();
        }

        public Guid? DepartmentId { get; set; }
    }
}