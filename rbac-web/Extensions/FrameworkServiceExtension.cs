using System.Text.Json.Serialization;
using rbac_core.Context;

namespace rbac_web.Extensions
{
    internal static class FrameWorkServiceExtension
    {
        internal static IServiceCollection AddFrameworkServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services
                .AddControllers()
                .AddJsonOptions(o =>
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                );

            services.AddDbContext<AppDbContext>();

            return services;
        }
    }
}
