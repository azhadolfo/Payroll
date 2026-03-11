using Payroll.API.Models;

namespace Payroll.API.Features.Accounts
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser user);
    }
}