using System.ComponentModel.DataAnnotations;

namespace Payroll.API.Features.Accounts.Dtos
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}