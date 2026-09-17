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
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;

        // აქ უსაფრთოხების მიზნით არაა კარგი პრაქტიკა რომ ვუთხრა პაროლის მოთხოვნებს არ აკმაყოფილებს ან ემაილი არ არის სწორი ფორმატით.
    }
}
