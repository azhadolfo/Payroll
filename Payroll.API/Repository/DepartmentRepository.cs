using Microsoft.EntityFrameworkCore;
using Payroll.API.Data;
using Payroll.API.Interfaces;
using Payroll.API.Models;

namespace Payroll.API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Department department, CancellationToken cancellationToken = default)
        {
            await _dbContext.Departments.AddAsync(department, cancellationToken);
        }

        public void Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
        }

        public async Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Departments.Include(d => d.Employees).ToListAsync(cancellationToken);
        }

        public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Departments.Include(d => d.Employees).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> IsDepartmentExist(Guid departmentId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Departments.AnyAsync(d => d.Id == departmentId, cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Update(Department department)
        {
            _dbContext.Departments.Update(department);
        }
    }
}