using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.CustomExceptions
{
    public class OutDatedRefreshTokenException:Exception
    {
        public OutDatedRefreshTokenException() : base("Refresh Token Is OutDated, You Must First LogIn In The Sysytem") { }
        public OutDatedRefreshTokenException(string message) : base(message) { }
        public OutDatedRefreshTokenException(string message, Exception inner) : base(message, inner) { }

    }
}
