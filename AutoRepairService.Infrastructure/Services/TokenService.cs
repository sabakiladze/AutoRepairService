using AutoRepairService.Domain.Entities;
using AutoRepairService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateAccessToken(User user)
        {
            throw new NotImplementedException();
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
