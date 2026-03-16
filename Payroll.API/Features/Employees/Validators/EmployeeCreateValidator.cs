using FluentValidation;
using Payroll.API.Features.Employees.Dtos;

namespace Payroll.API.Features.Employees.Validators
{
    public class EmployeeCreateValidator : AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeCreateValidator()
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