using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public  class CustomerProfile
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string DefaultAddress { get; set; } = null!;

        public User User { get; set; } = null!;

        public ICollection<PayMent> Payments { get; set; } = new List<PayMent>();
        public ICollection<Card> Cards { get; set; } = new List<Card>();
        public ICollection<Service> Services { get; set; } = new List<Service>();

    }
}
