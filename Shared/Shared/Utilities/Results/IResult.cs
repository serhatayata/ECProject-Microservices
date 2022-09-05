using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; }
        object Message { get; }
        int StatusCode { get; }
    }
}
