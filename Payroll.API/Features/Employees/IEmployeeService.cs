using Payroll.API.Common.Pagination;
using Payroll.API.Features.Employees.Dtos;
using Payroll.API.Features.Employees.Filters;

namespace Payroll.API.Features.Employees
{
    public interface IEmployeeService
    {
        Task<PagedResult<EmployeeResponseDto>> GetAllAsync(EmployeeFilter filter, CancellationToken cancellationToken = default);

        Task<EmployeeResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto createEmployeeDto, CancellationToken cancellationToken = default);

        Task<EmployeeResponseDto?> UpdateAsync(Guid id, EmployeeEditDto editEmployeeDto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}