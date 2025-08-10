using rbac_core.Context;

namespace rbac_web.Extensions
{
    internal static class AppServiceExtension
    {
        internal static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            return services;
        }
    }
}
