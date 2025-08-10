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

            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
