using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public class MechanicBankAccount
    {
        public Guid Id { get; set; }

        public Guid MechanicId {get; set; }
        public string IBAN { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string HolderName { get; set; } = null!;

        public bool IsDefault { get; set; }

        public MechanicProfile MechanicProfile { get; set; } = null!;

        public ICollection<PayMent> PayMents { get; set; } = new List<PayMent>();

    }
}
