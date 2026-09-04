using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public class Part
    {
        public Guid Id { get; set; }

        public string PartName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ICollection<ServicePart> ServiceParts { get; set; }
            = new List<ServicePart>();
    }
}
