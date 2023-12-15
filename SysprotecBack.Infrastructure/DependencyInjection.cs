namespace SysprotecBack.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Business.Interfaces.Services;
    using SysprotecBack.Infrastructure.DataAccess;
    using SysprotecBack.Infrastructure.Services;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<SysprotecContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IJwtService, JwtService>();

            return services;
        }
    }
}
