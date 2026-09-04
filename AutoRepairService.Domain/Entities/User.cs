using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutoRepairService.Domain.Entities
{
    public class User
    {

        public Guid Id { get; set; }

        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public MechanicProfile? MechanicProfile { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }
        public Profile? Profile { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
