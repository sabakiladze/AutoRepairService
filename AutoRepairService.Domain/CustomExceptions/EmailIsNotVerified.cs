using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.CustomExceptions
{
    public  class EmailIsNotVerified:Exception
    {
        public EmailIsNotVerified():base("Email Is Not Verified"){}
        public EmailIsNotVerified(string message):base(message){}
        public EmailIsNotVerified(string message, Exception inner) : base(message, inner) { }
    }
}
