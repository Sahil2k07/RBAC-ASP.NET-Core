using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using rbac_core.Context;
using rbac_core.Interface.Config;
using rbac_core.Settings;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private class DummyDbConfig(string connectionString) : IDbConfig
    {
        private readonly string _connectionString = connectionString;

        public string GetConnectionString() => _connectionString;
    }

    public AppDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("migration.json", optional: false, reloadOnChange: false)
            .Build();

        var dbSettings =
            config.GetSection("DbSettings").Get<DbSettings>()
            ?? throw new InvalidOperationException("Could not load DbSettings from configuration.");

        var connectionString =
            $"Server={dbSettings.Host};Database={dbSettings.Database};User Id={dbSettings.User};Password={dbSettings.Password};TrustServerCertificate=true;";

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options, new DummyDbConfig(connectionString));
    }
}
