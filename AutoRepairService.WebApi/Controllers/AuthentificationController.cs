using AutoRepairService.Application.Dtos.Authentication;
using AutoRepairService.Application.Dtos.UserDto;
using AutoRepairService.Application.ServiceInterfaces;
using AutoRepairService.Domain.Entities;
using AutoRepairService.Domain.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutoRepairService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController(IAuthentication authontificate, IRoleRepository rolerepository) : ControllerBase
    {
        private readonly IAuthentication _authentication = authontificate;
        private readonly IRoleRepository _roleRepository=rolerepository;

        [HttpPost("Register")]

        // IActionResult არის ინტერფეისი რომელიც აერთიანებს პასუხებს როგორიცაა badrequest, ok, notfound.
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            try
            {
                var result = await _authentication.RegisterAsync(dto);
                return Ok(new
                {
                    Message = "Successfylly registered!",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LoginRequestDto dto)
        {
            try
            {
                LoginResponseDto? result = await _authentication.LoginAsync(dto);
                return Ok(new
                {
                    Message = "Successfuly Logged In!",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut(string refreshtoken)
        {
            try
            {
                await _authentication.LogOutAsync(refreshtoken);
                return Ok("LoggedOut Sucessfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail(string verifyemailtoken)
        {
            try
            {
                bool veirfied = await _authentication.VerificationAsync(verifyemailtoken);
                return Ok("Your Email Is Verifyed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RequestRefreshTokenDto dto)
        {
            try
            {
                var user = await _authentication.RefreshTokenAsync(dto);// აბრუნებს loginresponse dto.
                return Ok(new { 
                Message="Refresh Token Generated Successfully",
                Data=user});

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount(DeleteUserDto dto)
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId is null) return Unauthorized(false);
            var result=await _authentication.DeleteAccountAsync(dto);    
            return Ok(result);

        }

        /// უნდა დავამატო წერილის გაგზავნა როდესაც ვაჭერ არაფერზე არ გადავყავარ.

        [HttpPost]
        public async Task<IActionResult> AddRoles(string role)
        {
            try
            {
                await _roleRepository.AddRole(role);
                return Ok();
            }
            catch(Exception ex) {return BadRequest(ex.Message); }
        }
        /// უნდა დავამატო როლები




    }
}
