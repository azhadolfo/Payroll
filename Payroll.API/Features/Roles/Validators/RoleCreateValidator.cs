using FluentValidation;
using Payroll.API.Features.Roles.Dtos;

namespace Payroll.API.Features.Roles.Validators
{
    public class RoleCreateValidator : AbstractValidator<RoleCreateDto>
    {
        public RoleCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}