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
        private readonly AppDbContext _appDbContext = appdbcontext;  // AppDbContext შეიცავს ყველა სიას. 

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

        }

        public async  Task<User?> GetByEmailAsync(string email)
        {
            return await _appDbContext.Users.Include(x=>x.UserRoles).ThenInclude(x=>x.Role).FirstOrDefaultAsync(x => x.Email == email);
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
        public async Task<User?> GetByVerificationTokenAsync(string token)
        {
            return await _appDbContext.Users
                .FirstOrDefaultAsync(x => x.EmailVerificationToken == token);
        }
        public void  Update(User user)
        {
             _appDbContext.Users.Update(user);

        }

    }
}
