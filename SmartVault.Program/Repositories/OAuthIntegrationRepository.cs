using SmartVault.Program.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace SmartVault.Program.Repositories
{
    public class OAuthIntegrationRepository : IOAuthIntegrationRepository
    {
        private readonly ApplicationDbContext _context;

        public OAuthIntegrationRepository(ApplicationDbContext context)
            => _context = context;

        public List<OAuthIntegration> GetAll()
            => _context.OAuthIntegrations.ToList();

        public OAuthIntegration GetById(int id)
            => _context.OAuthIntegrations.Find(id);

        public void Add(OAuthIntegration oAuthIntegration)
        {
            _context.OAuthIntegrations.Add(oAuthIntegration);
            _context.SaveChanges();
        }

        public void Update(OAuthIntegration oAuthIntegration)
        {
            _context.OAuthIntegrations.Update(oAuthIntegration);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.OAuthIntegrations.Find(id);
            if (entity != null)
            {
                _context.OAuthIntegrations.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}