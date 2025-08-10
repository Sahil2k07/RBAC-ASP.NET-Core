using rbac_core.Settings;

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
            var jwtSettingsSection = configs.GetSection("JwtSettings");

            services.Configure<DbSettings>(dbConfigs);
            services.Configure<JwtSettings>(jwtSettingsSection);

            return services;
        }
    }
}
