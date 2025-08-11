using Microsoft.EntityFrameworkCore;
using rbac_core.Entity;
using rbac_core.Interface.Config;

namespace rbac_core.Context
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options, IDbConfig dbConfig)
        : DbContext(options)
    {
        private readonly IDbConfig _dbConfig = dbConfig;

        public DbSet<User> Users { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _dbConfig?.GetConnectionString();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
