using Payroll.API.Common;

namespace Payroll.API.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string TableName { get; set; } = default!;
        public string RecordId { get; set; } = default!;
        public string FieldName { get; set; } = default!;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string ChangedBy { get; set; } = default!;
        public DateTime ChangedAt { get; set; }
        public AuditOperation Operation { get; set; }
    }
}