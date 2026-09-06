using AutoRepairService.Domain.Entities;
using AutoRepairService.Domain.Interfaces.RepositoryInterfaces;
using AutoRepairService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext appdbcontext) : IUserRepository
    {
        private readonly AppDbContext _appDbContext = appdbcontext;

        public async Task AddAsync(User user)
        {
            await _appDbContext.Users.AddAsync(user);

        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _appDbContext.Users
                .FirstOrDefaultAsync(x => x.Id==id);

            if (user is null)
                return;

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();

        }

        public async  Task<User?> GetByEmailAsync(string email)
        {
            return await _appDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async  Task<User?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshtoken)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshtoken);
        }

        public async Task UpdateAsync(User user)
        {
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
