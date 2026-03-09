using Microsoft.EntityFrameworkCore;
using Payroll.API.Common;
using Payroll.API.Features.Departments.Dtos;
using Payroll.API.Features.Departments.Filters;
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

        public async Task<PagedResult<DepartmentResponseDto>> GetAllAsync(DepartmentFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _departmentRepository.GetAllQuery();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(d => d.Name.ToLower().Contains(filter.Name));
            }

            var totalRecords = await query.CountAsync(cancellationToken);

            var departments = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<DepartmentResponseDto>
            {
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalRecords = totalRecords,
                Data = departments.Select(d => d.ToDto())
            };
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