using AutoRepairService.Application.Dtos.UserDto;
using AutoRepairService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.ServiceInterfaces
{
    public interface IAuthentication
    {
        Task<UserResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<UserResponseDto?> LoginAsync(LoginRequestDto dto );
        Task<bool> VerificationAsync(string token);
        Task LogOutAsync(string refreshtoken);// რადგან ვაკეტებთ გასვლას, უნდა ვიცოდეთ რომელი ტოკენი უნდა გავაუქმოთ


    }
}

//        User რეგისტრირდება
//       ↓
//ვამოწმებთ Email-ს
//       ↓
//Password → Hash
//       ↓
//User იქმნება
//       ↓
//Verification Token იქმნება
//       ↓
//SMTP-ით იგზავნება Email
//       ↓
//User აჭერს Verify Email-ს
//       ↓
//Email დადასტურებულია
//       ↓
//შეუძლია Login
