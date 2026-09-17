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
       
        

        // access ტოკენი საჭირო აღარ არის,
        // იმიტომ რომ ცოტახნიანი არის და ბაზაში შენახვას აზრ არ აქვს.
        // refreshtoken კი უნდა იყოს ბაზაში რადგან დიდი ხანი აქვს მას სიცოცხლე
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public bool IsEmailVerified { get; set; }

        public string? EmailVerificationToken { get; set; }

        public DateTime? EmailVerificationTokenExpiresAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public MechanicProfile? MechanicProfile { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }
        public Profile? Profile { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
