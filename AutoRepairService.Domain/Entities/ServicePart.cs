using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public class ServicePart
    {
        public Guid Id { get; set; }


        public Guid PartId { get; set; }
        public Guid ServiceId { get; set; }

        public Part Part { get; set; } = null!;
        public Service Service { get; set; } = null!;
        
        public int Quantity { get; set; }


    }
}
