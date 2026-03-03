using Payroll.API.Models;

namespace Payroll.API.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}