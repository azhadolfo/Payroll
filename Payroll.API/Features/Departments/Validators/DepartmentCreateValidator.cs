using FluentValidation;
using Payroll.API.Features.Departments.Dtos;

namespace Payroll.API.Features.Departments.Validators
{
    public class DepartmentCreateValidator : AbstractValidator<DepartmentCreateDto>
    {
        public DepartmentCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}