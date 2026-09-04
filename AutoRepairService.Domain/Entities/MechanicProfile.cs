using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public class MechanicProfile
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Specialization { get; set; } = null!;
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }
        public string? Bio { get; set; }

        public bool IsVerified { get; set; }
        public decimal Rating { get; set; }
        public bool IsAvailable { get; set; }

        public decimal Latitde { get; set; }
        public decimal Longitde { get; set; }

        public int CmpletedJobsCount { get; set; }
        public User User { get; set; } = null!;

        public ICollection<PayMent> Payments { get; set; }
    = new List<PayMent>();
        public ICollection<MechanicBankAccount> BankAccounts { get; set; } = new List<MechanicBankAccount>();
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
