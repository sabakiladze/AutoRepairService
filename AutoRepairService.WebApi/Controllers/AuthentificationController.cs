using AutoRepairService.Application.Dtos.UserDto;
using AutoRepairService.Application.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

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
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            
        }
    }
}
