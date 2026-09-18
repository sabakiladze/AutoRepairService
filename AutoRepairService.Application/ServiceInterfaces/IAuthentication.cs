using AutoRepairService.Application.Dtos.Authentication;
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
        Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto );
        Task<bool> VerificationAsync(string token);
        Task LogOutAsync(string refreshtoken);// რადგან ვაკეტებთ გასვლას, უნდა ვიცოდეთ რომელი ტოკენი უნდა გავაუქმოთ და გავხადოტ null
        Task<LoginResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto dto); //...
        Task<bool> DeleteAccountAsync(DeleteUserDto dto);


    }
}

