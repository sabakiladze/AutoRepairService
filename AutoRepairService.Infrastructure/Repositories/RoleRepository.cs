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
    public class RoleRepository(AppDbContext appdbcontext) : IRoleRepository
    {
        private readonly AppDbContext _appDbContext = appdbcontext;

        public async Task AddRole(string name)
        {
            Role role = new Role
            {
                RoleName=name
            };
             await _appDbContext.Roles.AddAsync(role);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(Guid roleId)
        {
            var role= await _appDbContext.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
            return role;
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            var role=await _appDbContext.Roles.FirstOrDefaultAsync(x=>x.RoleName.ToLower() == roleName.ToLower());
            return role;
        }

        public async Task<ICollection<Role>?> GetAllRolesAsync()
        {
            ICollection<Role> roles = await _appDbContext.Roles.ToListAsync(); 
            return roles;
        }

       
    }
}
