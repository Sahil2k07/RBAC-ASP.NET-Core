using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using rbac_core.Context;

namespace rbac_web.Extensions
{
    internal static class FrameWorkServiceExtension
    {
        internal static IServiceCollection AddFrameworkServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddHttpContextAccessor();

            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
                    httpContext =>
                        RateLimitPartition.GetSlidingWindowLimiter(
                            partitionKey: "global",
                            factory: key => new SlidingWindowRateLimiterOptions
                            {
                                PermitLimit = 100,
                                Window = TimeSpan.FromMinutes(1),
                                SegmentsPerWindow = 10,
                                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                                QueueLimit = 0,
                            }
                        )
                );
            });

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
