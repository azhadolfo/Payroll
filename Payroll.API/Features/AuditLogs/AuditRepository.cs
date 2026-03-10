using Payroll.API.Data;
using Payroll.API.Models;

namespace Payroll.API.Features.AuditLogs
{
    public class AuditRepository : IAuditRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuditRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRangeAsync(IEnumerable<AuditLog> logs, CancellationToken cancellationToken = default)
        {
            await _dbContext.AuditLogs.AddRangeAsync(logs, cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}