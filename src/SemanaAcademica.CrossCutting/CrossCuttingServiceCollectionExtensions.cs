using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemanaAcademica.CrossCutting.Cryptography;
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

            services.AddTransient<IUserContext, UserHttpContext>();
            services.AddTransient<ICryptoghaphy, CryptographyService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
