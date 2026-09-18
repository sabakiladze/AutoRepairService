using AutoRepairService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Interfaces.RepositoryInterfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task<Role?> GetRoleByIdAsync(Guid roleId);
        Task AddRole(string name);
        Task<ICollection<Role>?> GetAllRolesAsync();
    }
}
