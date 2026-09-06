using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.ServiceInterfaces
{
    public interface IEmailService
    {
        Task  SendVerificationEmailAsync(string email, string token);
    }
}
