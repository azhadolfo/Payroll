using Payroll.API.Dtos.Department;
using Payroll.API.Models;

namespace Payroll.API.Mappers
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