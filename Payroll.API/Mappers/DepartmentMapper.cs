using Payroll.API.Dtos.Department;
using Payroll.API.Models;

namespace Payroll.API.Mappers
{
    public static class DepartmentMapper
    {
        public static DepartmentDto ToDepartmentDto(this Department departmentModel)
        {
            return new DepartmentDto
            {
                Id = departmentModel.Id,
                Name = departmentModel.Name,
                //Employees = departmentModel.Employees.Select(e => e.ToEmployeeDto()).ToList()
            };
        }
    }
}
