using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartVault.Program.BusinessObjects;
using System.IO;

namespace SmartVault.Program
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<OAuthIntegration> OAuthIntegrations { get; set; }

        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
            => _configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var projectBasePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "SmartVault.DataGeneration", "bin", "Debug", "net5.0"));

                var connectionString = $"{string.Format(_configuration.GetConnectionString("DefaultConnection"), projectBasePath)}\\{_configuration["DatabaseFileName"]}";
                optionsBuilder.UseSqlite(connectionString);
            }
        }
    }
}
