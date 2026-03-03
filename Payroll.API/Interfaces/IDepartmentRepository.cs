using Payroll.API.Models;

namespace Payroll.API.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Department> AddAsync(Department department, CancellationToken cancellationToken = default);
        void UpdateAsync(Department department);
        void DeleteAsync(Department department);

    }
}
