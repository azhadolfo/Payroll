using Payroll.API.Models;

namespace Payroll.API.Features.AuditLogs
{
    public interface IAuditRepository
    {
        Task AddRangeAsync(IEnumerable<AuditLog> logs, CancellationToken cancellationToken = default);

        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}