using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Domain
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<NotificationContext>();

            return services;
        }
    }
}
