using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payroll.API.Features.Roles.Dtos;
using Payroll.API.Models;

namespace Payroll.API.Features.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RoleService> _logger;

        public RoleService(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<RoleService> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> AssignRoleToUserAsync(RoleAssignDto roleAssignDto, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(roleAssignDto.UserId);

            if (user == null)
                return false;

            if (!await _roleManager.RoleExistsAsync(roleAssignDto.RoleName))
                await _roleManager.CreateAsync(new IdentityRole(roleAssignDto.RoleName));

            var result = await _userManager.AddToRoleAsync(user, roleAssignDto.RoleName);

            _logger.LogInformation("Assigned role {RoleName} to user {Username}", roleAssignDto.RoleName, user.UserName);

            return result.Succeeded;
        }

        public async Task<bool> CreateRoleAsync(RoleCreateDto roleCreateDto, CancellationToken cancellationToken = default)
        {
            if (await _roleManager.RoleExistsAsync(roleCreateDto.Name))
                return false;

            var role = new IdentityRole(roleCreateDto.Name);
            var result = await _roleManager.CreateAsync(role);

            _logger.LogInformation("Created role {RoleName}", role.Name);

            return result.Succeeded;
        }

        public async Task<List<IdentityRole>?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _roleManager.Roles.ToListAsync(cancellationToken);
        }
    }
}