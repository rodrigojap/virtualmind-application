using VirtualMind.Domain.Enums;

namespace VirtualMind.Domain.Entities
{
    public class OperationCurrency
    {
        public int Id { get; set; }

        public Currency Currency { get; set; }

        public decimal Limit { get; set; }
    }
}
