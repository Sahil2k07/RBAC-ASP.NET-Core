namespace rbac_web.Extensions
{
    internal static class FrameWorkServiceExtension
    {
        internal static IServiceCollection AddFrameworkServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
