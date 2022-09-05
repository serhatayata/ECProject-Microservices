using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(object message, int statusCode = 200) : base(true, message, statusCode)
        {
        }

        public SuccessResult() : base(true, null, 200)
        {
        }
    }
}
