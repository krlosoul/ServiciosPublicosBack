namespace SysprotecBack.Api.Configurations
{
    using SysprotecBack.Infrastructure;
    using SysprotecBack.Business;

    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddBusiness();

            return services;
        }
    }
}
