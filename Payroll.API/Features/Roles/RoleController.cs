using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payroll.API.Features.Roles.Dtos;

namespace Payroll.API.Features.Roles
{
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto roleCreateDto, CancellationToken cancellationToken)
        {
            var success = await _roleService.CreateRoleAsync(roleCreateDto, cancellationToken);
            if (!success)
                return BadRequest("Role already exists or failed to create");

            return Ok("Role created");
        }

        [HttpPost("assign")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> AssignRole([FromBody] RoleAssignDto roleAssignDto, CancellationToken cancellationToken)
        {
            var success = await _roleService.AssignRoleToUserAsync(roleAssignDto, cancellationToken);

            if (!success)
                return BadRequest("Failed to assign role.");

            return Ok("Role assigned");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetAllAsync(cancellationToken);
            return Ok(roles?.Select(r => r.Name));
        }
    }
}