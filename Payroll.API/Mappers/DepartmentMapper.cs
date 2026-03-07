using Payroll.API.Dtos.Department;
using Payroll.API.Models;

namespace Payroll.API.Mappers
{
    public static class DepartmentMapper
    {
        public static DepartmentDto ToDto(this Department departmentModel)
        {
            return new DepartmentDto
            {
                Id = departmentModel.Id,
                Name = departmentModel.Name,
                //Employees = departmentModel.Employees.Select(e => e.ToEmployeeDto()).ToList()
            };
        }

        public static Department ToDepartmentFromCreateDTO(this CreateDepartmentDto departmentDto)
        {
            return new Department(departmentDto.Name);
        }
    }
}