using FluentValidation;
using Payroll.API.Features.Roles.Dtos;

namespace Payroll.API.Features.Roles.Validators
{
    public class RoleAssignValidator : AbstractValidator<RoleAssignDto>
    {
        public RoleAssignValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.RoleName)
                .NotEmpty();
        }
    }
}