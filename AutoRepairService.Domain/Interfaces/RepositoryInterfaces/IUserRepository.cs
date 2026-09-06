using AutoRepairService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
        Task<User?> GetByRefreshTokenAsync(string refreshtoken);
          
        //  როდესაც შევქმნი მის რეპოზიტორს უნდა გადავცე კონსტრუქტორს რეპოზიტორში AppDbContex

    }
}
