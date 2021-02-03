using System;

namespace VirtualMind.Domain.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }
    }
}
