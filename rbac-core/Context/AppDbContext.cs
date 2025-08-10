using Microsoft.EntityFrameworkCore;
using rbac_core.Interface.Config;

namespace rbac_core.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IDbConfig dbConfig)
        : DbContext(options)
    {
        private readonly IDbConfig _dbConfig = dbConfig;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _dbConfig.GetConnectionString();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
