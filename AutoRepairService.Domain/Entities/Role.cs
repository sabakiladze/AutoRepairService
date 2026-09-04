using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Entities
{
    public  class Role
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; } = null!;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        // ეს არის შუამავალი ცხრილი, რომელიც კავშირს აჩვენებს user და role შორის.
        //UserRole კლასში შეინახება მაშნ User and Role ობიექტები?
    }
}
