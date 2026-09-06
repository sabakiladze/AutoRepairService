using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.CustomExceptions
{
    public class EmailIsAleradyInUseException:Exception
    {
        public EmailIsAleradyInUseException(string message):base(message) { }
        public EmailIsAleradyInUseException() : base("This Email Is Already In Use") { }
        public EmailIsAleradyInUseException(string message, Exception inner) : base(message, inner) { }
    }
}
