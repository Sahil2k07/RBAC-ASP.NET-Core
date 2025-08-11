using rbac_core.Interface.Service;
using rbac_core.Service;

namespace rbac_web.Extensions
{
    internal static class AppServiceExtension
    {
        internal static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthService, AuthService>();

            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
