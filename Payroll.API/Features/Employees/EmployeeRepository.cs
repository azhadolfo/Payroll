using Microsoft.EntityFrameworkCore;
using Payroll.API.Data;
using Payroll.API.Models;

namespace Payroll.API.Features.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Employee department, CancellationToken cancellationToken = default)
        {
            await _dbContext.Employees.AddAsync(department, cancellationToken);
        }

        public void Delete(Employee department)
        {
            _dbContext.Employees.Remove(department);
        }

        public async Task<List<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Employees
                .Include(e => e.Department)
                .ToListAsync(cancellationToken);
        }

        public async Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Update(Employee department)
        {
            _dbContext.Employees.Update(department);
        }
    }
}