using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.Services;
using Application.Interfaces.Repositories;
using Infrastructure.Respositories;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, SystemDateTimeService>();
            services.AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        }
    }
}
