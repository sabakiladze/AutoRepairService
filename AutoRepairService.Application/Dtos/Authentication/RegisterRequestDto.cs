using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.Dtos.UserDto
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format. Must be a valid Gmail address.")]
        [RegularExpression(@"^[^@\s]+@gmail\.com$", ErrorMessage = "The email must be a valid @gmail.com address.")]

        public string Email { get; set; } = null!;



        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).+$", ErrorMessage = "Password must contain at least one uppercase letter and one special symbol.")]
        public string Password { get; set; } = null!;
    }
}
