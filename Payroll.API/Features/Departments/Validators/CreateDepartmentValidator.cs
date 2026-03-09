using FluentValidation;
using Payroll.API.Features.Departments.Dtos;

namespace Payroll.API.Features.Departments.Validators
{
    public class CreateDepartmentValidator : AbstractValidator<DepartmentCreateDto>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}