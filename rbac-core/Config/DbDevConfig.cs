using Microsoft.Extensions.Options;
using rbac_core.Interface.Config;

namespace rbac_core.Config
{
    public sealed class DbSettings
    {
        public required string Host { get; set; }
        public required string Database { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }
    }

    public sealed class DbDevConfig(IOptions<DbSettings> options) : IDbConfig
    {
        private readonly DbSettings _settings = options.Value;

        public string GetConnectionString()
        {
            return $"Server={_settings.Host};Database={_settings.Database};User Id={_settings.User};Password={_settings.Password};TrustServerCertificate=true";
        }
    }
}
