using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public class PayMent
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public Guid MechanicId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid ClientCardId { get; set; }
        public Guid MechanicAccountId { get; set; }

        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
        public string TransactionId { get; set; } = null!;
        public DateTime PaidAt { get; set; }

        public CustomerProfile Customer { get; set; } = null!;
        public MechanicProfile Mechanic { get; set; } = null!;
        public Service Service { get; set; } = null!;
        public Card ClientCard { get; set; } = null!;
        public MechanicBankAccount MechanicAccount { get; set; } = null!;
    }
}
