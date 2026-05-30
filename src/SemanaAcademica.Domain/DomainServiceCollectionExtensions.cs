using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemanaAcademica.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
