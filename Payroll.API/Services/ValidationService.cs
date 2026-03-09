using FluentValidation;

namespace Payroll.API.Services
{
    public class ValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ValidateAsync<T>(T model)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();

            if (validator is null)
                return;

            var result = await validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}