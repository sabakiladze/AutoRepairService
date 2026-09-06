using AutoRepairService.Application.ServiceInterfaces;
using AutoRepairService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Infrastructure
{
    public class UnitOfWork(AppDbContext AppDbContext) : IUnitOfWork
    {
        private readonly AppDbContext _appDbConetxt = AppDbContext;

        public async Task<int?> SaveChangesAsync()
        {
            return await _appDbConetxt.SaveChangesAsync();

        }
    }
}
