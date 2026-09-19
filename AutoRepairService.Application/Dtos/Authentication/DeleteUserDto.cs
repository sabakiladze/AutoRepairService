using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.Dtos.Authentication
{
    public  class DeleteUserDto
    {
        [Required(ErrorMessage ="Enter Your Current Password Do Confirm That You Want To Delete Your Email Permanently.")]
        public string? CurrentPassword { get; set; }
    }
}
