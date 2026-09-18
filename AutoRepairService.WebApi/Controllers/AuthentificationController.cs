using AutoRepairService.Application.Dtos.Authentication;
using AutoRepairService.Application.Dtos.UserDto;
using AutoRepairService.Application.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutoRepairService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public AuthentificationController(IAuthentication authontificate)
        {
            _authentication = authontificate;
        }

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
                LoginResponseDto result = await _authentication.LoginAsync(dto);
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
        public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
        {
            var token=await _authentication.R
        }

        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount(DeleteUserDto dto)
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId is null) return Unauthorized(false);
            var result=await _authentication.DeleteAccountAsync(dto);    
            return Ok(result);

        }
        



        // ცხრილების კონფიგურაციაში საჭიროა შევასწორო deleteon cascade რადგან თუ user წავშლი customerprofile იც უნდა წაიშალოს.
    }
}
