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
        Task LogOutAsync( RequestRefreshTokenDto dto);// რადგან ვაკეტებთ გასვლას, უნდა ვიცოდეთ რომელი ტოკენი უნდა გავაუქმოთ და გავხადოტ null
        Task<LoginResponseDto?> RefreshTokenAsync(RequestRefreshTokenDto dto); //...
        Task<bool> DeleteAccountAsync(Guid UserId, DeleteUserDto dto);


    }
}

