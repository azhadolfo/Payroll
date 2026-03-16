using Microsoft.AspNetCore.Identity;
using Payroll.API.Features.Roles.Dtos;

namespace Payroll.API.Features.Roles
{
    public interface IRoleService
    {
        Task<bool> CreateRoleAsync(RoleCreateDto roleCreateDto, CancellationToken cancellationToken = default);

        Task<bool> AssignRoleToUserAsync(RoleAssignDto roleAssignDto, CancellationToken cancellationToken = default);

        Task<List<IdentityRole>?> GetAllAsync(CancellationToken cancellationToken = default);
    }
}