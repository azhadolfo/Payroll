using Payroll.API.Features.Departments.Dtos;
using Payroll.API.Services;

namespace Payroll.API.Features.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ValidationService _validatorService;

        public DepartmentService(IDepartmentRepository departmentRepository,
            ValidationService validationService)
        {
            _departmentRepository = departmentRepository;
            _validatorService = validationService;
        }

        public async Task<DepartmentResponseDto> CreateAsync(DepartmentCreateDto createDepartmentDto, CancellationToken cancellationToken = default)
        {
            await _validatorService.ValidateAsync(createDepartmentDto);

            var department = createDepartmentDto.ToDepartmentFromCreateDTO();
            await _departmentRepository.AddAsync(department, cancellationToken);
            await _departmentRepository.SaveAsync(cancellationToken);
            return department.ToDto();
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Department not found");

            _departmentRepository.Delete(department);
            await _departmentRepository.SaveAsync(cancellationToken);
            return true;
        }

        public async Task<List<DepartmentResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var departments = await _departmentRepository.GetAllAsync(cancellationToken);
            return departments.Select(x => x.ToDto()).ToList();
        }

        public async Task<DepartmentResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Department not found");

            return department.ToDto();
        }

        public async Task<DepartmentResponseDto?> RenameAsync(Guid id, DepartmentEditDto editDepartmentDto, CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Department not found");

            department.Rename(editDepartmentDto.Name);
            _departmentRepository.Update(department);
            await _departmentRepository.SaveAsync(cancellationToken);
            return department.ToDto();
        }
    }
}