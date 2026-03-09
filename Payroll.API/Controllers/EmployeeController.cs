using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payroll.API.Dtos.Employee;
using Payroll.API.Services;

namespace Payroll.API.Controllers
{
    [ApiController]
    [Route("api/employee")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var employees = await _employeeService.GetAllAsync(cancellationToken);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeService.GetByIdAsync(id, cancellationToken);

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto createEmployeeDto, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeService.CreateAsync(createEmployeeDto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EmployeeEditDto editEmployeeDto, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeService.UpdateAsync(id, editEmployeeDto, cancellationToken);

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var success = await _employeeService.DeleteAsync(id, cancellationToken);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}