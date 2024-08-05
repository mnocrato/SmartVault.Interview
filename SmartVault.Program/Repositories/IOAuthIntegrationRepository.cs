using SmartVault.Program.BusinessObjects;
using System.Collections.Generic;

namespace SmartVault.Program.Repositories
{
    public interface IOAuthIntegrationRepository
    {
        List<OAuthIntegration> GetAll();
        OAuthIntegration GetById(int id);
        void Add(OAuthIntegration oAuthIntegration);
        void Update(OAuthIntegration oAuthIntegration);
        void Delete(int id);
    }
}