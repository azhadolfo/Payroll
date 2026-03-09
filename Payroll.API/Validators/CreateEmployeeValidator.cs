using FluentValidation;
using Payroll.API.Dtos.Employee;

namespace Payroll.API.Validators
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