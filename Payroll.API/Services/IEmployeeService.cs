using Payroll.API.Dtos.Employee;

namespace Payroll.API.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<EmployeeResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto createEmployeeDto, CancellationToken cancellationToken = default);

        Task<EmployeeResponseDto?> UpdateAsync(Guid id, EmployeeEditDto editEmployeeDto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}