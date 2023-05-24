using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotMessenger.Shared.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string? message) : base(message)
        {
            Code = 404;
            Detail = "Not found";
        }
    }
}
