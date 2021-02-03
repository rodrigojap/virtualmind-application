using VirtualMind.Domain.Enums;

namespace VirtualMind.Domain.Entities
{
    public class Operation : AuditableEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal RequestedAmount { get; set; }

        public decimal CurrentQuote { get; set; }

        public decimal PurchasedAmount { get; set; }

        public Currency Currency { get; set; }
    }
}
