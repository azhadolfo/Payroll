using Payroll.API.Features.Accounts.Dtos;

namespace Payroll.API.Features.Accounts
{
    public interface IAccountService
    {
        Task<NewUserDto> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken = default);

        Task<NewUserDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default);
    }
}