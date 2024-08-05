using System;

namespace SmartVault.Program.BusinessObjects
{
    public class OAuthIntegration : Entity
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
    }
}