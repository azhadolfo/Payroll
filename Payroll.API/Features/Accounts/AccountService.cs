using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payroll.API.Features.Accounts.Dtos;
using Payroll.API.Models;
using Payroll.API.Services;

namespace Payroll.API.Features.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ValidationService _validationService;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            ValidationService validationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _validationService = validationService;
        }

        public async Task<NewUserDto> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken = default)
        {
            await _validationService.ValidateAsync(registerDto);

            var appUser = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password!);

            if (!createdUser.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(", ", createdUser.Errors.Select(e => e.Description))
                );
            }

            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(", ", roleResult.Errors.Select(e => e.Description))
                );
            }

            return new NewUserDto
            {
                UserName = appUser.UserName,
                Email = appUser.Email,
                Token = _tokenService.CreateToken(appUser)
            };
        }

        public async Task<NewUserDto> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
        {
            await _validationService.ValidateAsync(loginDto);

            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.NormalizedUserName == loginDto.UserName.ToUpper(), cancellationToken)
                ?? throw new UnauthorizedAccessException("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Invalid username or password");

            return new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}