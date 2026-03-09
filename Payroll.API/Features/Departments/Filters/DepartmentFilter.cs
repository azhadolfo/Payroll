using Payroll.API.Common;

namespace Payroll.API.Features.Departments.Filters
{
    public class DepartmentFilter : PaginationQuery
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set => _name = value?.Trim().ToLower();
        }
    }
}