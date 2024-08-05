using NUnit.Framework;
using Moq;
using System.IO;
using System.Collections.Generic;
using SmartVault.Program.Services;
using Microsoft.Extensions.DependencyInjection;
using SmartVault.Program.Repositories;
using System.Linq;

namespace SmartVault.Tests
{
    [TestFixture]
    public class DocumentServiceTests
    {
        private DocumentService _documentService;
        private ServiceProvider _serviceProvider;
        private Mock<IDocumentRepository> _documentRepository;

        [SetUp]
        public void Setup()
        {
            _documentRepository = new Mock<IDocumentRepository>();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IDocumentService, DocumentService>();
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _documentService = new DocumentService(_documentRepository.Object!);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var path in
            from path in new[] { "file1.txt", "file2.txt", "file3.txt", "file4.txt", "file5.txt", "file6.txt", "output.txt" }
            where File.Exists(path)
            select path)
            {
                File.Delete(path);
            }

            _serviceProvider.Dispose();
        }

        [Test]
        public void GetAllFileSizes_ShouldCalculateTotalFileSize()
        {
            // Arrange
            var filePaths = new List<string> { "file1.txt", "file2.txt", "file3.txt" };
            foreach (var path in filePaths)
                File.WriteAllText(path, new string('a', 10));
            _documentRepository.Setup(repo => repo.GetAllDocumentsPaths()).Returns(filePaths);

            // Act
            var totalFileSize = _documentService.GetAllFileSizes();

            // Assert
            Assert.AreEqual(30, totalFileSize);
        }

        [Test]
        public void WriteEveryThirdFileToFile_ShouldWriteFilteredFilesToOutput()
        {
            // Arrange
            var accountId = "1";
            var documentsPath = new List<string>
            {
                "file1.txt",
                "file2.txt",
                "file3.txt",
                "file4.txt",
                "file5.txt",
                "file6.txt"
            };
            _documentRepository.Setup(repo => repo.GetDocumentsPathByAccountName(int.Parse(accountId)))
                                  .Returns(documentsPath);

            foreach (var path in documentsPath)
            {
                var content = path == "file3.txt" || path == "file6.txt" ? "Smith Property" : "Other Content";
                File.WriteAllText(path, content);
            }

            // Act
            _documentService.WriteEveryThirdFileToFile(accountId);

            // Assert
            var outputFilePath = "output.txt";
            var outputContent = File.ReadAllLines(outputFilePath);
            Assert.AreEqual(2, outputContent.Length);
            Assert.IsTrue(outputContent[0].Contains("Smith Property"));
            Assert.IsTrue(outputContent[1].Contains("Smith Property"));
        }
    }
}