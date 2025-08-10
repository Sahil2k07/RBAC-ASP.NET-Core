using rbac_core.Config;
using rbac_core.Interface.Config;

namespace rbac_web.Extensions
{
    internal static class EnvServiceExtension
    {
        internal static IServiceCollection AddEnvServices(
            this IServiceCollection services,
            IHostEnvironment env
        )
        {
            if (env.IsDevelopment())
            {
                services.AddSingleton<IDbConfig, DbDevConfig>();
            }
            else
            {
                services.AddSingleton<IDbConfig, DbProdConfig>();
            }

            return services;
        }
    }
}
