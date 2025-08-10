using rbac_core.Config;

namespace rbac_web.Extensions
{
    internal static class ConfigExtension
    {
        internal static IServiceCollection AddAppConfigs(
            this IServiceCollection services,
            IConfiguration configs
        )
        {
            var dbConfigs = configs.GetSection("DbSettings");

            services.Configure<DbSettings>(dbConfigs);

            return services;
        }
    }
}
