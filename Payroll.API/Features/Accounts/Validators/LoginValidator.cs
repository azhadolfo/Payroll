using FluentValidation;
using Payroll.API.Features.Accounts.Dtos;

namespace Payroll.API.Features.Accounts.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}