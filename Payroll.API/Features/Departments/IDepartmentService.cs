using Payroll.API.Common.Pagination;
using Payroll.API.Features.Departments.Dtos;
using Payroll.API.Features.Departments.Filters;

namespace Payroll.API.Features.Departments
{
    public interface IDepartmentService
    {
        Task<PagedResult<DepartmentResponseDto>> GetAllAsync(DepartmentFilter filter, CancellationToken cancellationToken = default);

        Task<DepartmentResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<DepartmentResponseDto> CreateAsync(DepartmentCreateDto createDepartmentDto, CancellationToken cancellationToken = default);

        Task<DepartmentResponseDto?> RenameAsync(Guid id, DepartmentEditDto editDepartmentDto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}