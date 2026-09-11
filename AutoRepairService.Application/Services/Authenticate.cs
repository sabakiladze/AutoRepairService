using AutoMapper;
using AutoRepairService.Application.Dtos.UserDto;
using AutoRepairService.Application.ServiceInterfaces;
using AutoRepairService.Domain.CustomExceptions;
using AutoRepairService.Domain.Entities;
using AutoRepairService.Domain.Interfaces.RepositoryInterfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.Services
{
    public class Authenticate(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailservice, IRoleRepository rolerepository) : IAuthentication
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService? _emailservice;
        private readonly IRoleRepository? _roleRepository;

        public async Task<UserResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user is null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            if (!user.IsEmailVerified)
                throw new EmailIsNotVerified();

;
            return _mapper.Map<UserResponseDto>(user);

            //ამასაც უნდა დავამატოთ jwt
        }

        public async Task LogOutAsync(string refreshtoken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(refreshtoken);
            
            if( user is null)
            {
                return;
            }


            user.RefreshToken = null;

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<UserResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

            if (existingUser is not null)
            {
                throw new EmailIsAleradyInUseException(dto.Email);
            }

            var role = await _roleRepository.GetRoleByNameAsync("Customer");

            if (role is null)
            {
                throw new Exception("Customer role was not found.");/// davamato exception
            }

            var user = _mapper.Map<User>(dto);
            await _unitOfWork.SaveChangesAsync();

            user.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(dto.Password);

            user.EmailVerificationToken =
                Guid.NewGuid().ToString("N");

            user.EmailVerificationTokenExpiresAt =
                DateTime.UtcNow.AddHours(24);


            user.Id = Guid.NewGuid();
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                User = user,
                Role = role
            };

            user.UserRoles.Add(userRole);

            await _userRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();

            await _emailservice.SendVerificationEmailAsync(
                user.Email,
                user.EmailVerificationToken);

            return _mapper.Map<UserResponseDto>(user);
        }
        public async Task<bool> VerificationAsync(string token)
        {
            var user= await _userRepository.GetByVerificationTokenAsync(token);
            if ((user is null || user.EmailVerificationTokenExpiresAt<DateTime.UtcNow))
            {
                return false;
            }
            user.IsEmailVerified = true;
            user.EmailVerificationToken = null;
            user.EmailVerificationTokenExpiresAt = null;

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}


//email / password
//      ↓
//User
//      ↓
//BCrypt
//      ↓
//Email verified?
//      ↓
//JWT
//      +
//RefreshToken
//      ↓
//DB
//      ↓
//LoginResponseDto


//1.LoginResponseDto
//        ↓
//2.ITokenService
//        ↓
//3.JwtSettings
//        ↓
//4.JwtTokenService
//        ↓
//5.RefreshTokenExpiresAt DB - ში
//        ↓
//6.LoginAsync - ში JWT + RefreshToken
//        ↓
//7.Program.cs → AddJwtBearer
//        ↓
//8. [Authorize]
//        ↓
//9.Refresh Token endpoint
//        ↓
//10. Logout