using rbac_core.Interface.Repository;
using rbac_core.Repository;

namespace rbac_web.Extensions
{
    internal static class AppRepositoryExtension
    {
        internal static IServiceCollection AddAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
