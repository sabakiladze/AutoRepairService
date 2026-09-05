using AutoRepairService.Domain.Entities;
using System;
using System.Collections.Generic;
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
          
        //  როდესაც შევქმნი მის რეპოზიტორს უნდა გადავცე კონსტრუქტორს რეპოზიტორში AppDbContex

    }
}
