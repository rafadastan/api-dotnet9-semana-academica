using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemanaAcademica.Domain.Contracts.Domain;
using SemanaAcademica.Domain.Entities;
using SemanaAcademica.Domain.Notifications;
using SemanaAcademica.Domain.Services;
using SemanaAcademica.Domain.Validators;

namespace SemanaAcademica.Domain
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<NotificationContext>();

            // Validators
            services.AddTransient<IValidator<CustomerEntity>, CustomerValidator>();
            services.AddTransient<IValidator<UserEntity>, UserValidator>();

            // Domain Services
            services.AddTransient<ICustomerDomainService, CustomerDomainService>();
            services.AddTransient<IUserDomainService, UserDomainService>();

            return services;
        }
    }
}