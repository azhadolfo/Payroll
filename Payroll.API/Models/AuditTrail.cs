namespace Payroll.API.Models
{
    public class AuditTrail
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        public string? Username { get; set; }

        public string TableName { get; set; } = string.Empty;

        public string ActionType { get; set; } = string.Empty;

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}