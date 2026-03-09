using Payroll.API.Models;

namespace Payroll.API.Features.Departments
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(Department department, CancellationToken cancellationToken = default);

        void Update(Department department);

        void Delete(Department department);

        Task SaveAsync(CancellationToken cancellationToken = default);

        Task<bool> IsDepartmentExist(Guid departmentId, CancellationToken cancellationToken = default);

        IQueryable<Department> GetAllQuery();
    }
}