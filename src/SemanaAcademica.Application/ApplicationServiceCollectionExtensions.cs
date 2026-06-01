using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemanaAcademica.Application.Contracts;
using SemanaAcademica.Application.Profile;
using SemanaAcademica.Application.Services;

namespace SemanaAcademica.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomerProfile>();
                cfg.AddProfile<UserProfile>();
            }).CreateMapper());

            services.AddTransient<ICustomerApplicationService, CustomerApplicationService>();
            services.AddTransient<IUserApplicationService, UserApplicationService>();

            return services;
        }
    }
}