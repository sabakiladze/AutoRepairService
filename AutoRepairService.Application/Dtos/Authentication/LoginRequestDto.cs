using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.Dtos.UserDto
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(250, ErrorMessage ="Maximum length of Email is 250 charachters")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;
    }
}
