using System.IO;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SmartVault.Program.Repositories;
using Microsoft.EntityFrameworkCore;
using SmartVault.Common;
using Microsoft.Extensions.Configuration;
using SmartVault.Program.Services;

namespace SmartVault.Program
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var provider = GetServiceProvider();
            if (args.Length == 0)
                return;

            using var scope = provider.CreateScope();
            var documentService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
            WriteEveryThirdFileToFile(args[0], documentService);
            GetAllFileSizes(documentService);
        }

        private static ServiceProvider GetServiceProvider()
        {
            var configuration = ConfigurationHelper.GetConfiguration();

            return new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
                {
                    var config = serviceProvider.GetRequiredService<IConfiguration>();
                    options.UseSqlite(ConfigurationHelper.GetConnectionString());
                })
                .AddScoped<IDocumentService, DocumentService>()
                .AddScoped<IDocumentRepository, DocumentRepository>()
                .AddScoped<IOAuthIntegrationRepository, OAuthIntegrationRepository>()
                .BuildServiceProvider();
        }

        private static void GetAllFileSizes(IDocumentService documentService)
        {
            var totalFileSize = documentService.GetAllFileSizes();
            Console.WriteLine($"Total file size: {totalFileSize} bytes");
        }

        private static void WriteEveryThirdFileToFile(string accountId, IDocumentService documentService)
            => documentService.WriteEveryThirdFileToFile(accountId);
    }
}