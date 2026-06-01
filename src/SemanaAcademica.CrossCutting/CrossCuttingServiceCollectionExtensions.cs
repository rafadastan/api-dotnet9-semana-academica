using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemanaAcademica.CrossCutting.Cryptography;
using SemanaAcademica.CrossCutting.Security.Service;
using SemanaAcademica.CrossCutting.Security.Settings;
using SemanaAcademica.CrossCutting.Security.UserContexts;
using SemanaAcademica.Domain.Contracts.CrossCutting.Cryptography;
using SemanaAcademica.Domain.Contracts.CrossCutting.Security.UserContext;

namespace SemanaAcademica.CrossCutting
{
    public static class CrossCuttingServiceCollectionExtensions
    {
        public static IServiceCollection AddSecurityDependencyInjection(
           this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            // Settings
            var accessTokenSettings = configuration
                .GetSection("AccessTokenSettings")
                .Get<AccessTokenSettings>()
                ?? throw new InvalidOperationException("AccessTokenSettings não configurado no appsettings.json.");

            services.AddSingleton(accessTokenSettings);

            // Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<AccessTokenService>();
            services.AddTransient<ICryptoghaphy, CryptographyService>();
            services.AddTransient<IUserContext, UserHttpContext>();

            return services;
        }
    }
}