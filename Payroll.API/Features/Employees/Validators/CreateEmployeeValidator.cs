using FluentValidation;
using Payroll.API.Features.Employees.Dtos;

namespace Payroll.API.Features.Employees.Validators
{
    public class CreateEmployeeValidator : AbstractValidator<EmployeeCreateDto>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.BasicSalary)
                .GreaterThan(0);
        }
    }
}