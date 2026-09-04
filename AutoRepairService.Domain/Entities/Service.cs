using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public Guid MechanicId { get; set; }
        public Guid VehicleId { get; set; }

        public string? Description { get; set; }

        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedByMechanicAt { get; set; }
        public DateTime? DoneAt { get; set; }

        public decimal Total { get; set; }

        public bool IsPaid { get; set; }

        public decimal? EstimatedHours { get; set; }

        public string? PaymentMethod { get; set; }

        public decimal ServicePrice { get; set; }
        public decimal PartsPrice { get; set; }

        public CustomerProfile Customer { get; set; } = null!;
        public MechanicProfile Mechanic { get; set; } = null!;
        public Vehicle Vehicle { get; set; } = null!;

        public ICollection<ServicePart> ServiceParts { get; set; }
            = new List<ServicePart>();

        public ICollection<PayMent> Payments { get; set; }
            = new List<PayMent>();
    }
}
