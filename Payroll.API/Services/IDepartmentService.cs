using Payroll.API.Dtos.Department;

namespace Payroll.API.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<DepartmentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<DepartmentDto> CreateAsync(CreateDepartmentDto createDepartmentDto, CancellationToken cancellationToken = default);

        Task<DepartmentDto?> RenameAsync(Guid id, EditDepartmentDto editDepartmentDto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}