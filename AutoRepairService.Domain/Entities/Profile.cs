using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public  class Profile
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public byte[]? ProfileImage { get; set; }

        public User User { get; set; } = null!;
    }
}
