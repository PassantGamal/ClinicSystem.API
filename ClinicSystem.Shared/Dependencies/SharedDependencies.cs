using FluentValidation;
using ClinicSystem.Shared.Validators.IdentityValidators;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;


namespace ClinicSystem.Shared.Dependencies
{
    public static class SharedDependencies
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(LoginValidator).Assembly);
            return services;
        }
    }
}