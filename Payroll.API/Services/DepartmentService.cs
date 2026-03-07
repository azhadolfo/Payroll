using Payroll.API.Dtos.Department;
using Payroll.API.Interfaces;
using Payroll.API.Mappers;

namespace Payroll.API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository, CancellationToken cancellationToken = default)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentResponseDto> CreateAsync(DepartmentCreateDto createDepartmentDto, CancellationToken cancellationToken = default)
        {
            var department = createDepartmentDto.ToDepartmentFromCreateDTO();
            await _departmentRepository.AddAsync(department, cancellationToken);
            await _departmentRepository.SaveAsync(cancellationToken);
            return department.ToDto();
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null) return false;

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
            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
            if (department == null) return null;
            return department.ToDto();
        }

        public async Task<DepartmentResponseDto?> RenameAsync(Guid id, DepartmentEditDto editDepartmentDto, CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
            if (department == null) return null;

            department.Rename(editDepartmentDto.Name);
            _departmentRepository.Update(department);
            await _departmentRepository.SaveAsync(cancellationToken);
            return department.ToDto();
        }
    }
}