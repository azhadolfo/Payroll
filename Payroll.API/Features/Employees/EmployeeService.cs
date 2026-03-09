using Payroll.API.Features.Departments;
using Payroll.API.Features.Employees.Dtos;
using Payroll.API.Services;

namespace Payroll.API.Features.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ValidationService _validationService;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            ValidationService validationService)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _validationService = validationService;
        }

        public async Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto createEmployeeDto, CancellationToken cancellationToken = default)
        {
            await _validationService.ValidateAsync(createEmployeeDto);

            if (!await _departmentRepository.IsDepartmentExist(createEmployeeDto.DepartmentId, cancellationToken))
            {
                throw new InvalidOperationException($"Department with an id {createEmployeeDto.DepartmentId} does not exist.");
            }

            var employee = createEmployeeDto.ToEmployeeFromCreateDto();
            await _employeeRepository.AddAsync(employee, cancellationToken);
            await _employeeRepository.SaveAsync(cancellationToken);
            return employee.ToDto();
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Employee not found");

            _employeeRepository.Delete(employee);
            await _employeeRepository.SaveAsync(cancellationToken);
            return true;
        }

        public async Task<List<EmployeeResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var employees = await _employeeRepository.GetAllAsync(cancellationToken);
            return employees.ConvertAll(e => e.ToDto());
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

            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAsync(cancellationToken);
            return employee.ToDto();
        }
    }
}