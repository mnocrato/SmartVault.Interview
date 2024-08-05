using System.Collections.Generic;
using System.Linq;

namespace SmartVault.Program.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
            => _context = context;

        public List<string> GetDocumentsPathByAccountName(int accountId)
            => (from doc in _context.Documents
                join acc in _context.Accounts on doc.AccountId equals acc.Id
                where acc.Id == accountId
                orderby doc.Id
                select doc.FilePath).ToList();

        public List<string> GetAllDocumentsPaths() => _context.Documents.Select(s => s.FilePath).ToList();
    }
}