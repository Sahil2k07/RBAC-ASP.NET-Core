using rbac_core.Interface.Config;

namespace rbac_core.Config
{
    public sealed class DbProdConfig : IDbConfig
    {
        public string GetConnectionString()
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            if (
                string.IsNullOrEmpty(host)
                || string.IsNullOrEmpty(database)
                || string.IsNullOrEmpty(user)
                || string.IsNullOrEmpty(password)
            )
            {
                throw new InvalidOperationException("Missing required DB environment variables.");
            }

            return $"Server={host};Database={database};User Id={user};Password={password};TrustServerCertificate=true";
        }
    }
}
