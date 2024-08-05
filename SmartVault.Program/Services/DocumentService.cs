using SmartVault.Program.Repositories;
using System.IO;
using System;
using System.Linq;

namespace SmartVault.Program.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
            => _documentRepository = documentRepository;

        public long GetAllFileSizes()
        {
            var filePaths = _documentRepository.GetAllDocumentsPaths();
            long totalFileSize = 0;

            foreach (var path in filePaths)
                if (File.Exists(path))
                    totalFileSize += new FileInfo(path).Length;

            return totalFileSize;
        }

        public void WriteEveryThirdFileToFile(string accountId)
        {
            var documentsPath = _documentRepository.GetDocumentsPathByAccountName(int.Parse(accountId));
            var filteredDocuments = documentsPath
                .Where((doc, index) => (index + 1) % 3 == 0 && File.ReadAllText(doc).Contains("Smith Property"))
                .ToList();

            var outputFilePath = "output.txt";

            using var writer = new StreamWriter(outputFilePath);
            foreach (var doc in filteredDocuments)
                writer.WriteLine(File.ReadAllText(doc));
        }
    }
}