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
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;
        }
    }
}