using Payroll.API.Common;

namespace Payroll.API.Features.AuditLogs
{
    public interface IAuditService
    {
        Task LogAsync<T>(
            T entity,
            string recordId,
            string changedBy,
            AuditOperation operation,
            CancellationToken cancellationToken = default);

        Task LogChangesAsync<T>(
            T oldEntity,
            T newEntity,
            string recordId,
            string changedBy,
            CancellationToken cancellationToken = default);
    }
}