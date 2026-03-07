using Payroll.API.Dtos.Employee;
using Payroll.API.Interfaces;
using Payroll.API.Mappers;

namespace Payroll.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<EmployeeResponseDto> CreateAsync(EmployeeCreateDto createEmployeeDto, CancellationToken cancellationToken = default)
        {
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
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);

            if (employee == null)
                return false;

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
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee == null) return null;
            return employee.ToDto();
        }

        public async Task<EmployeeResponseDto?> UpdateAsync(Guid id, EmployeeEditDto editEmployeeDto, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);

            if (employee == null)
                return null;

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