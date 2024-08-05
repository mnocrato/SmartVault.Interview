using System;

namespace SmartVault.Program.BusinessObjects
{
    public abstract class Entity
    {
        public DateTime CreatedOn { get; set; }
        
        protected Entity()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
