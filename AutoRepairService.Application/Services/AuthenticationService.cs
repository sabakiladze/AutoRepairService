using AutoMapper;
using AutoRepairService.Application.Dtos.Authentication;
using AutoRepairService.Application.Dtos.UserDto;
using AutoRepairService.Application.ServiceInterfaces;
using AutoRepairService.Domain.CustomExceptions;
using AutoRepairService.Domain.Entities;
using AutoRepairService.Domain.Interfaces;
using AutoRepairService.Domain.Interfaces.RepositoryInterfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.Services
{
    public class AuthenticationService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, IRoleRepository rolerepository, ITokenService tokenservice) : IAuthentication
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailservice=emailService;
        private readonly IRoleRepository _roleRepository=rolerepository;
        private readonly ITokenService _tokenService=tokenservice;



        public async Task<bool> DeleteAccountAsync(DeleteUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId) ?? throw new UserNotFoundException();

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash))
                throw new EmailOrPasswordIsIncorrectException();

            await _userRepository.DeleteAsync(dto.UserId);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }



        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email) ?? throw new EmailOrPasswordIsIncorrectException();

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new EmailOrPasswordIsIncorrectException();

            if (!user.IsEmailVerified)
                throw new EmailIsNotVerified();

            string refreshtoken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshtoken;
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(2);

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync();


            return _mapper.Map<LoginResponseDto>(user);

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

            // აქ რეფრესჰტოკენს ვაუქმებ და მის გარეშე არაფრის უფლება არ ექნება კონტროლერში.
            // შეიძ₾ება ოიფიქრო რომ 7 დღე არ გასულა მაგრამ როდესაც ახლიდან დავლოგინდებით მერე თავიდან შევქმნით 
            // refreshtoken-ს. არ წავშლით მას იმ შემთხვევაში თუ სულ შესული ვიქნებით. ამ შემთხვევაში refreshtoken
            // შევინარჩუნებ და მისით დავაგენერირებ accesstoken.

        }





        public async Task<LoginResponseDto?> RefreshTokenAsync(RequestRefreshTokenDto dto)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(dto.RefreshToken) ?? throw new UserNotFoundException(); // if user is null ??(then)
            if (user.RefreshTokenExpiresAt is null ||
                user.RefreshTokenExpiresAt <= DateTime.UtcNow)  // რეფრესჰ ტოკენს ვადა თუ გაუვიდა ახალი ტოკენის გენერირება ჯერ არ შეიძლება, ჯერ უნდა დალოგინდე.
            {
               throw new OutDatedRefreshTokenException(); 
            }

            string accesstoken = _tokenService.GenerateAccessToken(user);
            string refreshtoken=_tokenService.GenerateRefreshToken();

            user.RefreshToken= refreshtoken;
            user.RefreshTokenExpiresAt =DateTime.UtcNow.AddDays(7);

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync();

            return new LoginResponseDto
            {
                User = user,
                RefreshToken = refreshtoken,
                AccessToken = accesstoken
            };


        }



        public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

            if (existingUser is not null)
            {
                throw new EmailIsAleradyInUseException(dto.Email);
            }

            var role = await _roleRepository.GetRoleByNameAsync("Customer") ?? throw new Exception("Customer role was not found.");
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

            return _mapper.Map<LoginResponseDto>(user);
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