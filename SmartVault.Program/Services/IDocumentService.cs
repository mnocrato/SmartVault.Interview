namespace SmartVault.Program.Services
{
    public interface IDocumentService
    {
        long GetAllFileSizes();
        void WriteEveryThirdFileToFile(string accountId);
    }
}