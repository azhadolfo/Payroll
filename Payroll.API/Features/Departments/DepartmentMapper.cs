using Payroll.API.Features.Departments;
using Payroll.API.Features.Departments.Dtos;
using Payroll.API.Features.Employees;
using Payroll.API.Models;

namespace Payroll.API.Features.Departments
{
    public static class DepartmentMapper
    {
        public static DepartmentResponseDto ToDto(this Department departmentModel)
        {
            return new DepartmentResponseDto
            {
                Id = departmentModel.Id,
                Name = departmentModel.Name,
                Employees = departmentModel.Employees.ConvertAll(e => e.ToDto())
            };
        }

        public static Department ToDepartmentFromCreateDTO(this DepartmentCreateDto departmentDto)
        {
            return new Department(departmentDto.Name);
        }
    }
}