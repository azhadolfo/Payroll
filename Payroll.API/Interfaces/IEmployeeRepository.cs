using Payroll.API.Models;

namespace Payroll.API.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(Employee department, CancellationToken cancellationToken = default);

        void Update(Employee department);

        void Delete(Employee department);

        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}