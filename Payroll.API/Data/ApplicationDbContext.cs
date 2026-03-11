using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Payroll.API.Common;
using Payroll.API.Models;
using Payroll.API.Services;
using System.Text.Json;

namespace Payroll.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService _currentUser;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUser) : base(options)
        {
            _currentUser = currentUser;
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AuditTrail> AuditTrails => Set<AuditTrail>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "18588f65-97f3-4a41-b73f-3d5a6c7fa0d0",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "bbe16d99-2a17-463e-be2c-2d41769a5131"
                },
                new IdentityRole
                {
                    Id = "b99c2763-8b22-4cbe-b3d7-88aa10190f86",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "554bcbf6-be81-4aa2-b174-adfdad43e9e5"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<Department>()
                .HasIndex(d => d.Name)
                .IsUnique();

            builder.Entity<Employee>()
            .HasIndex(e => e.EmployeeNumber)
            .IsUnique();

            builder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = new List<AuditTrail>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditTrail || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var audit = new AuditTrail
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = _currentUser.UserId,
                    Username = _currentUser.Username,
                    Timestamp = DateTime.UtcNow
                };

                if (entry.State == EntityState.Added)
                {
                    audit.ActionType = AuditActionType.Create.ToString();
                    audit.NewValues = JsonSerializer.Serialize(entry.CurrentValues.ToObject());
                }

                if (entry.State == EntityState.Modified)
                {
                    audit.ActionType = AuditActionType.Update.ToString();
                    audit.OldValues = JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                    audit.NewValues = JsonSerializer.Serialize(entry.CurrentValues.ToObject());
                }

                if (entry.State == EntityState.Deleted)
                {
                    audit.ActionType = AuditActionType.Delete.ToString();
                    audit.OldValues = JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                }

                auditEntries.Add(audit);
            }

            if (auditEntries.Count > 0)
                await AuditTrails.AddRangeAsync(auditEntries, cancellationToken);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}