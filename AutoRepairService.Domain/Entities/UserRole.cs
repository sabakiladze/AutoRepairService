using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public  class UserRole
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; } //public Guid UserId { get; set; } = User.Id; ეს ასე იმიტომ არ შემიძლია დავწერო, რომ როდესაც როლი UserRole შეიქმნება შეიძლება user and role საერთოდ არ იყოს მინიჭებული.


        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
