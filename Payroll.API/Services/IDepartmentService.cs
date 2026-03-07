using Payroll.API.Dtos.Department;

namespace Payroll.API.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<DepartmentResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<DepartmentResponseDto> CreateAsync(DepartmentCreateDto createDepartmentDto, CancellationToken cancellationToken = default);

        Task<DepartmentResponseDto?> RenameAsync(Guid id, DepartmentEditDto editDepartmentDto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}