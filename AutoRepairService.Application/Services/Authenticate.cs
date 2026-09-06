using AutoMapper;
using AutoRepairService.Application.Dtos.UserDto;
using AutoRepairService.Application.ServiceInterfaces;
using AutoRepairService.Domain.CustomExceptions;
using AutoRepairService.Domain.Entities;
using AutoRepairService.Domain.Interfaces.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.Services
{
    public class Authenticate(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailservice) : IAuthentication
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailservice;

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

            await _userRepository.UpdateAsync(user);

            await _unitOfWork.SaveChangesAsync();
            // ეს უნდა დავასრულო მას შემდეგ რაც, შევქმნი JWT აუთენთიფიკაციას.
        }

        public async Task<UserResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var existingUser=await _userRepository.GetByEmailAsync(dto.Email);

            if (existingUser is not null)
            {
                throw new EmailIsAleradyInUseException(nameof(existingUser));
            }

            var user = _mapper.Map<User>(dto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserResponseDto>(user);

            // ამას უნდა დავამატო ემაილის გაგაზავნა.

            

            //user.IsEmailVerified = false;
            //user.EmailVerificationToken = Guid.NewGuid().ToString("N");
            //user.EmailVerificationTokenExpiresAt = DateTime.UtcNow.AddHours(24);

            //await _userRepository.AddAsync(user);
            //await _unitOfWork.SaveChangesAsync();

            //await _emailService.SendVerificationEmailAsync(
            //    user.Email,
            //    user.EmailVerificationToken);

            //return _mapper.Map<UserResponseDto>(user);

            // ზემოთ რაც წერია ისინი უნდა გავაკეთო smtp ისთვის მმაგრა ისედაც დეფაუტ მნიშვნელობები მაქვს და რაღა საჭიროა ეს
        }

        public Task<bool> VerificationAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}
