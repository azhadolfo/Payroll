using System.Security.Claims;

namespace Payroll.API.Services
{
    public interface IUserContextService
    {
        string GetUsername();
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUsername()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            return user?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        }
    }
}