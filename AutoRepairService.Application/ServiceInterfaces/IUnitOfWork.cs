using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.ServiceInterfaces
{
    public interface IUnitOfWork
    {
        Task<int?> SaveChangesAsync();
        // როდესაც ბაზაში იცვლება რაიმე,
        // იმის მიხედვით თუ რამდენი რამე შეიცვალა აბრუნებს რიცხვს.
        // ამიტომ ვაბრუნებინებთ რიცხვს.
    }
}
