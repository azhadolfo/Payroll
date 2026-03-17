using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Payroll.API.Features.Employees.Dtos;
using Payroll.API.Features.Employees.Filters;

namespace Payroll.API.Features.Employees
{
    [ApiController]
    [Route("api/employees")]
    [EnableRateLimiting("api")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] EmployeeFilter filter, CancellationToken cancellationToken = default)
        {
            var employees = await _employeeService.GetAllAsync(filter, cancellationToken);
            return Ok(employees);
        }

        [Authorize(Policy = "AdminOnly")]
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