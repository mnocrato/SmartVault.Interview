using System;

namespace SmartVault.Program.BusinessObjects
{
    public partial class User : Entity
    {
        public string FullName => $"{FirstName} {LastName}";
    }
}
