using Payroll.API.Models;

namespace Payroll.API.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(Guid id);
        Task<Department> CreateAsync(string name);
        Task<Department?> RenameAsync(Guid id, string newName);
        Task<bool> DeleteAsync(Guid id);
    }
}
