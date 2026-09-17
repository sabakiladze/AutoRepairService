using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Domain.CustomExceptions
{
    public  class EmailOrPasswordIsIncorrectException: Exception
    {
        public EmailOrPasswordIsIncorrectException() : base("Email Or Password Is Incorrect"){ }
        public EmailOrPasswordIsIncorrectException(string message) : base(message) { }
        public EmailOrPasswordIsIncorrectException(string message, Exception inner) : base(message, inner) { }
    }
}
