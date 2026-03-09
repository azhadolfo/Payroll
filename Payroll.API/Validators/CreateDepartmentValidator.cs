using FluentValidation;
using Payroll.API.Dtos.Department;

namespace Payroll.API.Validators
{
    public class CreateDepartmentValidator : AbstractValidator<DepartmentCreateDto>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}