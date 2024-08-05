using System.Collections.Generic;

namespace SmartVault.Program.Repositories
{
    public interface IDocumentRepository
    {
        List<string> GetAllDocumentsPaths();
        List<string> GetDocumentsPathByAccountName(int accountId);
    }
}