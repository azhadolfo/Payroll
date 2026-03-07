using Microsoft.AspNetCore.Mvc;
using Payroll.API.Dtos.Department;
using Payroll.API.Services;

namespace Payroll.API.Controllers
{
    [ApiController]
    [Route("api/department")]
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

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentCreateDto createDepartmentDto, CancellationToken cancellationToken)
        {
            try
            {
                var department = await _departmentService.CreateAsync(createDepartmentDto, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Rename(Guid id, [FromBody] DepartmentEditDto editDepartmentDto, CancellationToken cancellationToken)
        {
            try
            {
                var department = await _departmentService.RenameAsync(id, editDepartmentDto, cancellationToken);
                if (department == null) return NotFound();
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _departmentService.DeleteAsync(id, cancellationToken);

                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}