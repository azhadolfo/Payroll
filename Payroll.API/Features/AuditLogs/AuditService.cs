using Payroll.API.Common;
using Payroll.API.Models;
using System.Reflection;

namespace Payroll.API.Features.AuditLogs
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        // For Create and Delete
        public async Task LogAsync<T>(
            T entity,
            string recordId,
            string changedBy,
            AuditOperation operation,
            CancellationToken cancellationToken = default)
        {
            var tableName = typeof(T).Name;

            var logs = new List<AuditLog>();

            if (operation == AuditOperation.Create || operation == AuditOperation.Delete)
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.GetMethod != null);

                foreach (var prop in properties)
                {
                    logs.Add(new AuditLog
                    {
                        Id = Guid.NewGuid(),
                        TableName = tableName,
                        RecordId = recordId,
                        FieldName = prop.Name,
                        OldValue = operation == AuditOperation.Create ? null : prop.GetValue(entity)?.ToString(),
                        NewValue = operation == AuditOperation.Create ? prop.GetValue(entity)?.ToString() : null,
                        ChangedBy = changedBy,
                        ChangedAt = DateTime.UtcNow,
                        Operation = operation
                    });
                }
            }

            if (logs.Any())
            {
                await _auditRepository.AddRangeAsync(logs, cancellationToken);
                await _auditRepository.SaveAsync(cancellationToken);
            }
        }

        public async Task LogChangesAsync<T>(
            T oldEntity,
            T newEntity,
            string recordId,
            string changedBy,
            CancellationToken cancellationToken = default)
        {
            var tableName = typeof(T).Name;
            var logs = new List<AuditLog>();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetMethod != null);

            foreach (var prop in properties)
            {
                var oldValue = prop.GetValue(oldEntity)?.ToString();
                var newValue = prop.GetValue(newEntity)?.ToString();

                if (oldValue != newValue)
                {
                    logs.Add(new AuditLog
                    {
                        Id = Guid.NewGuid(),
                        TableName = tableName,
                        RecordId = recordId,
                        FieldName = prop.Name,
                        OldValue = oldValue,
                        NewValue = newValue,
                        ChangedBy = changedBy,
                        ChangedAt = DateTime.UtcNow,
                        Operation = AuditOperation.Update
                    });
                }
            }

            if (logs.Any())
            {
                await _auditRepository.AddRangeAsync(logs, cancellationToken);
                await _auditRepository.SaveAsync(cancellationToken);
            }
        }
    }
}