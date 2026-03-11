using Microsoft.EntityFrameworkCore;
using Payroll.API.Common;
using Payroll.API.Features.Departments;
using Payroll.API.Features.Employees.Dtos;
using Payroll.API.Features.Employees.Filters;
using Payroll.API.Services;

namespace Payroll.API.Features.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IValidationService _validationService;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IValidationService validationService,
            ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _validationService = validationService;
            _logger = logger;
        }

        public async Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto createEmployeeDto, CancellationToken cancellationToken = default)
        {
            await _validationService.ValidateAsync(createEmployeeDto);

            if (!await _departmentRepository.IsDepartmentExist(createEmployeeDto.DepartmentId, cancellationToken))
            {
                throw new InvalidOperationException($"Department with an id {createEmployeeDto.DepartmentId} does not exist.");
            }

            _logger.LogInformation("Creating employee {EmployeeNumber} in department {DepartmentId}",
                createEmployeeDto.EmployeeNumber, createEmployeeDto.DepartmentId);

            var employee = createEmployeeDto.ToEmployeeFromCreateDto();
            await _employeeRepository.AddAsync(employee, cancellationToken);
            await _employeeRepository.SaveAsync(cancellationToken);

            return employee.ToDto();
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Employee not found");

            _logger.LogInformation("Deleting employee {EmployeeId}", id);

            _employeeRepository.Delete(employee);
            await _employeeRepository.SaveAsync(cancellationToken);
            return true;
        }

        public async Task<PagedResult<EmployeeResponseDto>> GetAllAsync(EmployeeFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _employeeRepository.GetAllQuery();

            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(e => e.FirstName.ToLower().Contains(filter.FirstName));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(e => e.LastName.ToLower().Contains(filter.LastName));
            }

            if (filter.DepartmentId.HasValue)
            {
                query = query.Where(e => e.DepartmentId == filter.DepartmentId);
            }

            var totalRecords = await query.CountAsync(cancellationToken);

            var employees = await query
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<EmployeeResponseDto>
            {
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalRecords = totalRecords,
                Data = employees.ConvertAll(e => e.ToDto())
            };
        }

        public async Task<EmployeeResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Employee not found");

            return employee.ToDto();
        }

        public async Task<EmployeeResponseDto?> UpdateAsync(Guid id, EmployeeEditDto editEmployeeDto, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Employee not found");

            employee.EmployeeNumber = editEmployeeDto.EmployeeNumber;
            employee.FirstName = editEmployeeDto.FirstName;
            employee.LastName = editEmployeeDto.LastName;
            employee.SetSalary(editEmployeeDto.BasicSalary);

            if (employee.DepartmentId != editEmployeeDto.DepartmentId)
            {
                if (!await _departmentRepository.IsDepartmentExist(editEmployeeDto.DepartmentId, cancellationToken))
                {
                    throw new InvalidOperationException($"Department with an id {editEmployeeDto.DepartmentId} does not exist.");
                }

                employee.ChangeDepartment(editEmployeeDto.DepartmentId);
            }

            _logger.LogInformation("Updating employee {EmployeeId}", id);

            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync(cancellationToken);
            return employee.ToDto();
        }
    }
}