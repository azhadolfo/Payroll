namespace Payroll.API.Common.Authorization
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddApiAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("HRAccess", policy =>
                    policy.RequireRole("HR", "Admin"));

                options.AddPolicy("EmployeeAccess", policy =>
                    policy.RequireRole("Employee", "Admin", "HR"));
            });

            return services;
        }
    }
}