using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public class Card
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string CardHolderName { get; set; } = null!;
        public string Last4Digits { get; set; } = null!;
        public string CardBrand { get; set; } = null!;
        public string ProcessorToken { get; set; } = null!;

        public bool IsDefault { get; set; }

        public CustomerProfile Customer { get; set; } = null!;

        public ICollection<PayMent> Payments { get; set; } = new List<PayMent>();

        //აქ ერთი მნიშვნელოვანი მომენტია: შენს SQL-ში Card.Customer_Id პირდაპირ Customer_Profile_Table(Users_Id)-ს 
        //    უკავშირდება და არა Customer_Profile_Table(Id)-ს.

        // ამიტომ Infrastructure-ში mapping-ში ეს აუცილებლად უნდა გავითვალისწინოთ.


    }
}
