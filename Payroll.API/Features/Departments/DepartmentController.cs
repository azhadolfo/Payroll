using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payroll.API.Features.Departments.Dtos;

namespace Payroll.API.Features.Departments
{
    [ApiController]
    [Route("api/department")]
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var departments = await _departmentService.GetAllAsync(cancellationToken);
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetByIdAsync(id, cancellationToken);

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDto createDepartmentDto, CancellationToken cancellationToken)
        {
            var department = await _departmentService.CreateAsync(createDepartmentDto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Rename(Guid id, [FromBody] DepartmentEditDto editDepartmentDto, CancellationToken cancellationToken)
        {
            var department = await _departmentService.RenameAsync(id, editDepartmentDto, cancellationToken);
            return Ok(department);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var success = await _departmentService.DeleteAsync(id, cancellationToken);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}