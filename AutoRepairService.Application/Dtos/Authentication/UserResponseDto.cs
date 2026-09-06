using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.Dtos.UserDto
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
