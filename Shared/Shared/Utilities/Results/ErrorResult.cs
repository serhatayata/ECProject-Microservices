using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(object message, int statusCode = 400) : base(false, message, statusCode)
        {

        }

        public ErrorResult() : base(false, null, 400)
        {

        }
    }
}
