using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SmartVault.Program.BusinessObjects;
using SmartVault.Common;
using SmartVault.Program;

namespace SmartVault.DataGeneration
{

    partial class Program
    {
        static void Main(string[] args)
        {
            var configuration = ConfigurationHelper.GetConfiguration();

            // Ensure the database file is created
            File.WriteAllText(configuration["DatabaseFileName"], string.Empty);

            using var context = new ApplicationDbContext(configuration);
            context.Database.EnsureCreated();

            var users = new List<User>();
            var accounts = new List<Account>();
            var documents = new List<Document>();

            var random = new Random();
            var now = DateTime.Now;
            var startDate = new DateTime(1985, 1, 1);
            var dateRange = (DateTime.Today - startDate).Days;
            var fileInfo = new FileInfo("TestDoc.txt");
            var filePath = fileInfo.FullName;
            var fileLength = (int)fileInfo.Length;

            for (int i = 0; i < 100; i++)
            {
                var dob = startDate.AddDays(random.Next(dateRange));

                users.Add(new User
                {
                    Id = i,
                    FirstName = $"FName{i}",
                    LastName = $"LName{i}",
                    DateOfBirth = dob,
                    AccountId = i,
                    Username = $"UserName-{i}",
                    Password = "e10adc3949ba59abbe56e057f20f883e",
                    CreatedOn = now
                });

                accounts.Add(new Account
                {
                    Id = i,
                    Name = $"Accounts{i}",
                    CreatedOn = now
                });

                var documentNamePrefix = $"Documents{i}-";
                for (int d = 0; d < 10000; d++)
                {
                    var documentId = i * 10000 + d;
                    documents.Add(new Document
                    {
                        Id = documentId,
                        Name = $"{documentNamePrefix}{d}.txt",
                        FilePath = filePath,
                        Length = fileLength,
                        AccountId = i,
                        CreatedOn = now
                    });
                }
            }

            context.BulkInsert(users);
            context.BulkInsert(accounts);
            context.BulkInsert(documents);

            var accountData = context.Accounts.Count();
            Console.WriteLine($"AccountCount: {accountData}");
            var documentData = context.Documents.Count();
            Console.WriteLine($"DocumentCount: {documentData}");
            var userData = context.Users.Count();
            Console.WriteLine($"UserCount: {userData}");
        }
    }
}
