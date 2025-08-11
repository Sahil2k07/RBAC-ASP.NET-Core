using rbac_core.Middleware;

namespace rbac_web.Extensions
{
    internal static class WebAppExtension
    {
        internal static WebApplication UseAppConfigurations(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRateLimiter();

            app.MapControllers();

            app.UseMiddleware<HttpMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
