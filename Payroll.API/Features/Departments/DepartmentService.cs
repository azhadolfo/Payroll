using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payroll.API.Common;
using Payroll.API.Features.AuditLogs;
using Payroll.API.Features.Departments.Dtos;
using Payroll.API.Features.Departments.Filters;
using Payroll.API.Models;
using Payroll.API.Services;

namespace Payroll.API.Features.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IValidationService _validatorService;
        private readonly ILogger<DepartmentService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuditService _auditService;
        private readonly IUserContextService _userContextService;

        public DepartmentService(IDepartmentRepository departmentRepository,
            IValidationService validationService,
            ILogger<DepartmentService> logger,
            UserManager<ApplicationUser> userManager,
            IAuditService auditService,
            IUserContextService userContextService)
        {
            _departmentRepository = departmentRepository;
            _validatorService = validationService;
            _logger = logger;
            _userManager = userManager;
            _auditService = auditService;
            _userContextService = userContextService;
        }

        public async Task<DepartmentResponseDto> CreateAsync(DepartmentCreateDto createDepartmentDto, CancellationToken cancellationToken = default)
        {
            await _validatorService.ValidateAsync(createDepartmentDto);

            var department = createDepartmentDto.ToDepartmentFromCreateDTO();
            await _departmentRepository.AddAsync(department, cancellationToken);
            await _departmentRepository.SaveAsync(cancellationToken);

            // Audit log
            await _auditService.LogAsync(
                department,
                department.Id.ToString(),
                _userContextService.GetUsername(),
                AuditOperation.Create,
                cancellationToken);

            _logger.LogInformation("Created department {DepartmentName}", createDepartmentDto.Name);

            return department.ToDto();
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException("Department not found");

            _departmentRepository.Delete(department);
            await _departmentRepository.SaveAsync(cancellationToken);

            // Audit log
            await _auditService.LogAsync(
                department,
                id.ToString(),
                _userContextService.GetUsername(),
                AuditOperation.Delete,
                cancellationToken);

            _logger.LogInformation("Deleted department {DepartmentId}", id);
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
                .OrderBy(d => d.Name)
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

            var oldDepartment = department.Clone();

            department.Rename(editDepartmentDto.Name);
            _departmentRepository.Update(department);
            await _departmentRepository.SaveAsync(cancellationToken);

            // Audit log for update
            await _auditService.LogChangesAsync(
                oldDepartment,
                department,
                id.ToString(),
                _userContextService.GetUsername(),
                cancellationToken);

            _logger.LogInformation("Renamed department {DepartmentId}", id);
            return department.ToDto();
        }
    }
}