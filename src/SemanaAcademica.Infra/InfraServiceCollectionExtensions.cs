using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemanaAcademica.Domain.Contracts.Infra;
using SemanaAcademica.Domain.Contracts.Uow;
using SemanaAcademica.Infra.Context;
using SemanaAcademica.Infra.Repositories;
using SemanaAcademica.Infra.UOW;

namespace SemanaAcademica.Infra
{
    public static class InfraServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase("SemanaAcademicaDb"));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();  // ← faltava
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}