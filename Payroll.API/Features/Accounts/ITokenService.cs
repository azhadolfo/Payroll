using Payroll.API.Models;

namespace Payroll.API.Features.Accounts
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}