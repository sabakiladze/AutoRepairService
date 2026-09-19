using AutoRepairService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.Dtos.Authentication
{
    public  class LoginResponseDto
    {
        public UserResponseDto? User { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }

        // აქ იმიტომაა საჭირო ეს ტოკენები, რომ accesstoken-ის საშუალებით ხვდება სისტემა ვინაა შესული, რადგან მაასში არის მოთავსებული ობიექტის ემაილი, აიდი და როლი.
        // refreshtoke იმისთვის არის საჭირო რომ სისტემამ წაიღოს ის, შეამოწმოს და ახალი accesstoken დააგენერიროს

        // ტოკენები იმისთვისაა საჭირო, რომ მო


//        რefresh Token-ის პირველი გენერირება → Login-ის დროს.

//შემდეგი გამოყენება → Access Token-ის ვადის გასვლისას.

//და ჩვეულებრივ Refresh Token-ს ყოველ request-ზე არ აგზავნი.
    }
}
